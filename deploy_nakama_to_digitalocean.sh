#!/bin/bash

# SoreLosers Nakama Deployment to DigitalOcean
# Cost: ~$6/month for basic droplet
# Perfect for indie multiplayer games with 10-50 concurrent players

set -e

echo "🎯 SoreLosers Nakama Deployment to DigitalOcean"
echo "=============================================="
echo "💰 COST: ~$6/month for 1GB droplet"
echo "🎮 CAPACITY: 10-50 concurrent players"
echo ""

# Configuration
DROPLET_NAME="sorelosers-nakama"
DROPLET_SIZE="s-1vcpu-1gb"  # $6/month
DROPLET_REGION="nyc1"       # New York (change to your region)
DROPLET_IMAGE="ubuntu-22-04-x64"

echo "📋 Deployment Configuration:"
echo "   • Droplet Name: $DROPLET_NAME"
echo "   • Size: $DROPLET_SIZE ($6/month)"
echo "   • Region: $DROPLET_REGION"
echo "   • Image: $DROPLET_IMAGE"
echo ""

# Check if authenticated
echo "🔑 Checking DigitalOcean authentication..."
if ! doctl account get > /dev/null 2>&1; then
    echo "❌ Not authenticated with DigitalOcean"
    echo "Run: doctl auth init"
    exit 1
fi
echo "✅ DigitalOcean authentication successful"

# Check if SSH key exists
echo "🔐 Checking SSH key..."
SSH_KEY_NAME="sorelosers-deploy-key"
SSH_KEY_ID=""

# Check if SSH key already exists and get its ID
if doctl compute ssh-key list --format Name,ID | grep -q "$SSH_KEY_NAME"; then
    SSH_KEY_ID=$(doctl compute ssh-key list --format Name,ID | grep "$SSH_KEY_NAME" | awk '{print $2}')
    echo "✅ SSH key already exists: $SSH_KEY_NAME (ID: $SSH_KEY_ID)"
else
    echo "🔧 Creating SSH key for deployment..."
    
    # Generate SSH key if it doesn't exist
    SSH_KEY_PATH="$HOME/.ssh/sorelosers_deploy"
    if [ ! -f "$SSH_KEY_PATH" ]; then
        ssh-keygen -t rsa -b 4096 -f "$SSH_KEY_PATH" -N "" -C "sorelosers-deployment"
        echo "✅ SSH key generated: $SSH_KEY_PATH"
    fi
    
    # Add SSH key to DigitalOcean and get its ID
    SSH_KEY_ID=$(doctl compute ssh-key import "$SSH_KEY_NAME" --public-key-file "$SSH_KEY_PATH.pub" --format ID --no-header)
    echo "✅ SSH key uploaded to DigitalOcean (ID: $SSH_KEY_ID)"
fi

# Create droplet
echo "🚀 Creating DigitalOcean droplet..."
if doctl compute droplet list --format Name | grep -q "$DROPLET_NAME"; then
    echo "⚠️  Droplet $DROPLET_NAME already exists"
    DROPLET_IP=$(doctl compute droplet list --format Name,PublicIPv4 | grep "$DROPLET_NAME" | awk '{print $2}')
    echo "📍 Existing droplet IP: $DROPLET_IP"
else
    doctl compute droplet create "$DROPLET_NAME" \
        --size "$DROPLET_SIZE" \
        --image "$DROPLET_IMAGE" \
        --region "$DROPLET_REGION" \
        --ssh-keys "$SSH_KEY_ID" \
        --user-data-file <(cat << 'EOF'
#!/bin/bash
# SoreLosers Nakama Server Setup Script
# This runs automatically when the droplet is created

set -e

echo "🎮 Setting up SoreLosers Nakama server..."

# Update system
apt-get update
apt-get upgrade -y

# Install Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sh get-docker.sh
usermod -aG docker root

# Install Docker Compose
curl -L "https://github.com/docker/compose/releases/download/v2.21.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose

# Create Nakama directory
mkdir -p /home/nakama
cd /home/nakama

# Create docker-compose.yml for Nakama
cat > docker-compose.yml << 'COMPOSE_EOF'
version: '3'
services:
  postgres:
    container_name: postgres
    image: postgres:14-alpine
    environment:
      - POSTGRES_DB=nakama
      - POSTGRES_PASSWORD=localdb
    volumes:
      - data:/var/lib/postgresql/data
    expose:
      - "5432"
    ports:
      - "5432:5432"
    healthcheck:
      test: ["CMD", "pg_isready", "-U", "postgres", "-d", "nakama"]
      interval: 3s
      timeout: 3s
      retries: 5

  nakama:
    container_name: nakama
    image: registry.heroiclabs.com/heroiclabs/nakama:3.21.1
    entrypoint:
      - "/bin/sh"
      - "-ecx"
      - >
          /nakama/nakama migrate up --database.address postgres:localdb@postgres:5432/nakama &&
          exec /nakama/nakama --name nakama1 --database.address postgres:localdb@postgres:5432/nakama --logger.level INFO --session.token_expiry_sec 7200 --metrics.prometheus_port 9100 --console.port 7351 --console.username admin --console.password password
    restart: unless-stopped
    links:
      - "postgres:db"
    depends_on:
      postgres:
        condition: service_healthy
    volumes:
      - /home/nakama/data:/nakama/data
    expose:
      - "7349"
      - "7350"
      - "7351"
      - "9100"
    ports:
      - "7349:7349"
      - "7350:7350"
      - "7351:7351"
      - "9100:9100"
    healthcheck:
      test: ["CMD", "/nakama/nakama", "healthcheck"]
      interval: 10s
      timeout: 5s
      retries: 5

volumes:
  data:
COMPOSE_EOF

# Start Nakama services
docker-compose up -d

# Setup firewall
ufw allow ssh
ufw allow 7350/tcp  # Nakama client port
ufw allow 7351/tcp  # Nakama console port
ufw --force enable

echo "✅ SoreLosers Nakama server setup complete!"
echo "🎮 Nakama client port: 7350"
echo "🔧 Nakama console: http://$(curl -s ifconfig.me):7351"
echo "📊 Metrics port: 9100"

# Log completion
echo "$(date): Nakama setup completed successfully" >> /var/log/sorelosers-setup.log
EOF
)

    echo "⏳ Waiting for droplet to be created..."
    sleep 10
    
    # Get droplet IP
    DROPLET_IP=$(doctl compute droplet list --format Name,PublicIPv4 | grep "$DROPLET_NAME" | awk '{print $2}')
    echo "✅ Droplet created successfully!"
fi

echo ""
echo "🎉 DEPLOYMENT SUCCESSFUL!"
echo "=================================="
echo "💰 Monthly Cost: ~$6"
echo "📍 Server IP: $DROPLET_IP"
echo "🎮 Nakama Client: $DROPLET_IP:7350"
echo "🔧 Nakama Console: http://$DROPLET_IP:7351"
echo "   Username: admin"
echo "   Password: password"
echo ""
echo "🎯 Next Steps:"
echo "1. Wait 2-3 minutes for setup to complete"
echo "2. Update your NakamaManager.cs:"
echo "   ProductionHost = \"$DROPLET_IP\""
echo "   ProductionPort = 7350"
echo "   ProductionUseSSL = false"
echo "3. Rebuild your macOS app with production settings"
echo "4. Test multiplayer!"
echo ""
echo "🔍 To check setup progress:"
echo "   ssh root@$DROPLET_IP 'tail -f /var/log/sorelosers-setup.log'" 