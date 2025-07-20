#!/bin/bash

# SoreLosers Production Multiplayer Test Script
# Tests the DigitalOcean Nakama server and guides multiplayer testing

echo "🎮 SoreLosers Production Multiplayer Testing"
echo "==========================================="
echo ""

SERVER_IP="159.223.189.139"
SERVER_PORT="7350"
CONSOLE_PORT="7351"

# Test 1: Check if Nakama server is responding
echo "🔍 Test 1: Checking Nakama server..."
if curl -s --connect-timeout 5 http://$SERVER_IP:$SERVER_PORT/ >/dev/null 2>&1; then
    echo "✅ Nakama server is READY!"
    SERVER_READY=true
else
    echo "⏳ Nakama server still setting up..."
    echo "💡 This usually takes 3-5 minutes for first boot"
    SERVER_READY=false
fi

echo ""

# Test 2: Check admin console
echo "🔧 Test 2: Checking admin console..."
if curl -s --connect-timeout 5 http://$SERVER_IP:$CONSOLE_PORT/ >/dev/null 2>&1; then
    echo "✅ Admin console is available at: http://$SERVER_IP:$CONSOLE_PORT"
    echo "   Username: admin"
    echo "   Password: password"
else
    echo "⏳ Admin console not ready yet"
fi

echo ""

if [ "$SERVER_READY" = true ]; then
    echo "🎉 SERVER IS READY FOR TESTING!"
    echo "================================"
    echo ""
    echo "🎮 Multiplayer Test Steps:"
    echo "1. Open your production game:"
    echo "   open builds/SoreLosers-Production.app"
    echo ""
    echo "2. Test single instance first:"
    echo "   - Click 'Host Game'"
    echo "   - Note the room code"
    echo "   - Verify connection to production server"
    echo ""
    echo "3. Test multiplayer (open second instance):"
    echo "   - Run: open -n builds/SoreLosers-Production.app"
    echo "   - Click 'Join Game'"
    echo "   - Enter the room code from step 2"
    echo "   - Verify both players can see each other"
    echo ""
    echo "4. Test game synchronization:"
    echo "   - Play cards from both instances"
    echo "   - Verify moves sync between players"
    echo "   - Test chat functionality"
    echo ""
    echo "📊 Monitor server activity:"
    echo "   - Admin console: http://$SERVER_IP:$CONSOLE_PORT"
    echo "   - Server logs: ssh root@$SERVER_IP 'docker logs nakama'"
    echo ""
else
    echo "⏳ WAITING FOR SERVER SETUP..."
    echo "=============================="
    echo ""
    echo "💡 What's happening on your server:"
    echo "1. Installing Docker (1-2 minutes)"
    echo "2. Downloading Nakama + PostgreSQL images (1-2 minutes)"
    echo "3. Starting services (1 minute)"
    echo ""
    echo "🔄 Re-run this script to check progress:"
    echo "   ./test_production_multiplayer.sh"
    echo ""
    echo "🔍 Or check manually:"
    echo "   curl http://$SERVER_IP:$SERVER_PORT"
fi

echo ""
echo "💰 Monthly cost: \$6 (DigitalOcean droplet)"
echo "🎯 Capacity: 10-50 concurrent players" 