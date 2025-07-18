#!/bin/bash

# Test AWS Server Connectivity
# Quick test to verify the server is accessible

AWS_IP="3.16.16.22"
SERVER_PORT="7777"
SSH_KEY_PATH="~/Downloads/sorelosers-server-key.pem"

echo "🔍 Testing SoreLosers Server Connectivity"
echo "========================================"
echo "Target: $AWS_IP:$SERVER_PORT"

# Test SSH connectivity first (more reliable than ping)
echo ""
echo "1️⃣ Testing SSH connectivity..."
if ssh -i $SSH_KEY_PATH -o ConnectTimeout=10 -o StrictHostKeyChecking=no ubuntu@$AWS_IP "echo 'SSH connection successful'" 2>/dev/null; then
    echo "   ✅ SSH connection successful"
else
    echo "   ❌ SSH connection failed"
    echo "   💡 Check: Instance running? Key permissions? Security groups?"
    exit 1
fi

# Test basic connectivity  
echo ""
echo "2️⃣ Testing basic connectivity..."
if ping -c 3 $AWS_IP > /dev/null 2>&1; then
    echo "   ✅ Server is reachable via ping"
else
    echo "   ⚠️  Server ping failed (this is often disabled on AWS)"
fi

# Test port accessibility
echo ""
echo "3️⃣ Testing port $SERVER_PORT accessibility..."
if nc -z -w5 $AWS_IP $SERVER_PORT 2>/dev/null; then
    echo "   ✅ Port $SERVER_PORT is open and accepting connections"
else
    echo "   ❌ Port $SERVER_PORT is not accessible"
    echo "   💡 Check security groups and firewall settings"
fi

# Test if it's a Godot server
echo ""
echo "4️⃣ Testing Godot networking protocol..."
timeout 5 nc $AWS_IP $SERVER_PORT < /dev/null
if [ $? -eq 0 ]; then
    echo "   ✅ Server is responding to connections"
else
    echo "   ⚠️  Server may not be running or not responding"
fi

echo ""
echo "📋 Summary:"
echo "   🌐 Server IP: $AWS_IP"
echo "   🔌 Port: $SERVER_PORT"
echo "   🎮 Protocol: Godot ENet"
echo ""
echo "🎯 To connect from game:"
echo "   1. Start the SoreLosers client"
echo "   2. Click 'Join Game'"
echo "   3. Enter any 6-digit room code"
echo "   4. Server will create a new room if it doesn't exist" 