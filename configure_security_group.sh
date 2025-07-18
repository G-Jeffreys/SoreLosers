#!/bin/bash

# Configure AWS Security Group for SoreLosers Game Server
# This script adds the necessary inbound rule for port 7777

echo "🔧 Configuring AWS Security Group for SoreLosers"
echo "=============================================="

# You need to replace these values with your actual ones:
INSTANCE_ID="i-xxxxxxxxx"  # Replace with your EC2 instance ID
SECURITY_GROUP_ID="sg-xxxxxxxxx"  # Replace with your security group ID

echo "📋 Configuration:"
echo "   Instance ID: $INSTANCE_ID"
echo "   Security Group: $SECURITY_GROUP_ID"
echo "   Port: 7777 (UDP)"

# Get the current security group if not provided
if [ "$SECURITY_GROUP_ID" = "sg-xxxxxxxxx" ]; then
    echo ""
    echo "🔍 Finding security group for instance..."
    SECURITY_GROUP_ID=$(aws ec2 describe-instances --instance-ids $INSTANCE_ID --query 'Reservations[0].Instances[0].SecurityGroups[0].GroupId' --output text)
    echo "   Found: $SECURITY_GROUP_ID"
fi

# Add the inbound rule for port 7777
echo ""
echo "➕ Adding inbound rule for port 7777..."
aws ec2 authorize-security-group-ingress \
    --group-id $SECURITY_GROUP_ID \
    --protocol udp \
    --port 7777 \
    --cidr 0.0.0.0/0 \
    --tag-specifications "ResourceType=security-group-rule,Tags=[{Key=Description,Value=SoreLosers Game Server}]"

if [ $? -eq 0 ]; then
    echo "   ✅ Successfully added inbound rule for port 7777"
else
    echo "   ❌ Failed to add inbound rule (may already exist)"
fi

echo ""
echo "🎯 Next Steps:"
echo "   1. Wait 1-2 minutes for changes to take effect"
echo "   2. Test connectivity: ./test_server.sh"
echo "   3. Connect from your game client!" 