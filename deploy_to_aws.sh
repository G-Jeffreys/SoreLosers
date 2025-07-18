#!/bin/bash

# SoreLosers AWS Deployment Script
# Upload and configure dedicated server on AWS EC2

AWS_IP="3.16.16.22"
AWS_USER="ubuntu"  # Default for Ubuntu 22.04 EC2 instances
SERVER_DIR="/opt/sorelosers"
SSH_KEY_PATH="~/Downloads/sorelosers-server-key.pem"  # Update this to your SSH key path

echo "🚀 SoreLosers AWS Deployment Script"
echo "======================================"
echo "Target: $AWS_USER@$AWS_IP"
echo "Server Directory: $SERVER_DIR"

# CRITICAL FIX: Use proper server build script that excludes NetworkManager
echo ""
echo "🏗️ Building dedicated server with correct configuration..."
if [ ! -f "build_server.sh" ]; then
    echo "❌ Error: build_server.sh not found!"
    echo "   This script is required to build the server without NetworkManager conflicts"
    exit 1
fi

./build_server.sh
if [ $? -ne 0 ]; then
    echo "❌ Server build failed! Check build_server.sh output above."
    exit 1
fi

# Check if build exists
if [ ! -f "builds/sorelosers_server.x86_64" ]; then
    echo "❌ Error: Server build not found after build_server.sh execution!"
    echo "   Expected: builds/sorelosers_server.x86_64"
    exit 1
fi

echo ""
echo "✅ Server build completed successfully!"
echo "📋 Files to upload:"
echo "   ✓ builds/sorelosers_server.x86_64 (Main executable)"
echo "   ✓ builds/sorelosers_server.pck (Game assets)"
echo "   ✓ builds/data_SoreLosers_linuxbsd_x86_64/ (.NET runtime)"
echo "   ✓ server_config.json (Server configuration)"

# Create server directory on AWS
echo ""
echo "📁 Creating server directory on AWS..."
ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP "sudo mkdir -p $SERVER_DIR && sudo chown $AWS_USER:$AWS_USER $SERVER_DIR"

# Upload server files
echo ""
echo "📤 Uploading server files..."
scp -i $SSH_KEY_PATH builds/sorelosers_server.x86_64 $AWS_USER@$AWS_IP:$SERVER_DIR/
scp -i $SSH_KEY_PATH builds/sorelosers_server.pck $AWS_USER@$AWS_IP:$SERVER_DIR/
scp -i $SSH_KEY_PATH server_config.json $AWS_USER@$AWS_IP:$SERVER_DIR/
scp -i $SSH_KEY_PATH -r builds/data_SoreLosers_linuxbsd_x86_64/ $AWS_USER@$AWS_IP:$SERVER_DIR/

# Make executable
echo ""
echo "🔧 Setting permissions..."
ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP "chmod +x $SERVER_DIR/sorelosers_server.x86_64"

# Create systemd service for auto-start
echo ""
echo "⚙️ Creating systemd service..."
ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP << 'EOF'
sudo tee /etc/systemd/system/sorelosers.service > /dev/null << 'SERVICE'
[Unit]
Description=SoreLosers Game Server
After=network.target

[Service]
Type=simple
User=ubuntu
WorkingDirectory=/opt/sorelosers
ExecStart=/opt/sorelosers/sorelosers_server.x86_64 --headless
Restart=always
RestartSec=10
StandardOutput=journal
StandardError=journal

[Install]
WantedBy=multi-user.target
SERVICE

sudo systemctl daemon-reload
sudo systemctl enable sorelosers
EOF

echo ""
echo "🔥 Starting server..."
ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP "sudo systemctl restart sorelosers"

echo ""
echo "📊 Checking server status..."
ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP "sudo systemctl status sorelosers --no-pager"

echo ""
echo "✅ Deployment complete!"
echo "📡 Server running at: $AWS_IP:7777"
echo "🔍 To check logs: ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP 'sudo journalctl -u sorelosers -f'"
echo "🛑 To restart: ssh -i $SSH_KEY_PATH $AWS_USER@$AWS_IP 'sudo systemctl restart sorelosers'"
echo ""
echo "🔧 CRITICAL FIX APPLIED: Server now excludes NetworkManager autoload"
echo "   This should resolve RPC checksum failures between client and server" 