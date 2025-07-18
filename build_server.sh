#!/bin/bash

# SoreLosers Server Build Script
# Builds dedicated server using project_server.godot (excludes NetworkManager autoload)
# This prevents RPC checksum conflicts between client NetworkManager and server DedicatedServer

set -e  # Exit on any error

echo "🏗️ SoreLosers Dedicated Server Build Script"
echo "============================================"

# Configuration
BUILD_DIR="builds"
SERVER_EXECUTABLE="sorelosers_server.x86_64"
SERVER_PCK="sorelosers_server.pck"
GODOT_PATH="/Applications/Godot_mono.app/Contents/MacOS/Godot"

# Verify project_server.godot exists
if [ ! -f "project_server.godot" ]; then
    echo "❌ Error: project_server.godot not found!"
    echo "   This file is required for server builds to exclude NetworkManager"
    exit 1
fi

echo "📋 Build Configuration:"
echo "   ✓ Using project_server.godot (NetworkManager excluded)"
echo "   ✓ Target: ${BUILD_DIR}/${SERVER_EXECUTABLE}"
echo "   ✓ Main scene: DedicatedServer.tscn"
echo "   ✓ Godot path: ${GODOT_PATH}"

# Create builds directory
mkdir -p "$BUILD_DIR"

# CRITICAL FIX: Always delete existing build files first
# This prevents silent export failures that were wasting hours of debugging
echo ""
echo "🧹 Cleaning previous build files..."
rm -f "${BUILD_DIR}/${SERVER_EXECUTABLE}"
rm -f "${BUILD_DIR}/${SERVER_PCK}"
rm -rf "${BUILD_DIR}/data_SoreLosers_linuxbsd_x86_64"

# Backup original project.godot
echo ""
echo "💾 Backing up original project.godot..."
cp project.godot project.godot.backup

# Copy server-specific configuration
echo "🔄 Using server configuration (NetworkManager excluded)..."
cp project_server.godot project.godot

# Build server with server-specific configuration
echo ""
echo "🏗️ Building dedicated server..."
echo "   Command: ${GODOT_PATH} --headless --export-release \"Linux Server\" ${BUILD_DIR}/${SERVER_EXECUTABLE}"

"$GODOT_PATH" --headless --export-release "Linux Server" "${BUILD_DIR}/${SERVER_EXECUTABLE}"
BUILD_RESULT=$?

# Restore original project.godot
echo ""
echo "🔄 Restoring original project.godot..."
mv project.godot.backup project.godot

# Check build result
if [ $BUILD_RESULT -ne 0 ]; then
    echo "❌ Build failed with exit code: $BUILD_RESULT"
    exit $BUILD_RESULT
fi

# Verify build files were created
echo ""
echo "✅ Verifying build output..."

if [ -f "${BUILD_DIR}/${SERVER_EXECUTABLE}" ]; then
    echo "   ✓ Server executable: ${BUILD_DIR}/${SERVER_EXECUTABLE}"
    ls -lh "${BUILD_DIR}/${SERVER_EXECUTABLE}"
else
    echo "   ❌ Server executable not found!"
    exit 1
fi

if [ -f "${BUILD_DIR}/${SERVER_PCK}" ]; then
    echo "   ✓ Game assets: ${BUILD_DIR}/${SERVER_PCK}"
    ls -lh "${BUILD_DIR}/${SERVER_PCK}"
else
    echo "   ❌ Game assets file not found!"
    exit 1
fi

if [ -d "${BUILD_DIR}/data_SoreLosers_linuxbsd_x86_64" ]; then
    echo "   ✓ .NET runtime: ${BUILD_DIR}/data_SoreLosers_linuxbsd_x86_64/"
    du -sh "${BUILD_DIR}/data_SoreLosers_linuxbsd_x86_64"
else
    echo "   ❌ .NET runtime directory not found!"
    exit 1
fi

# Display file timestamps for verification (critical for debugging silent failures)
echo ""
echo "📅 Build timestamps (verify these are current):"
echo "   Server executable: $(stat -f "%Sm" "${BUILD_DIR}/${SERVER_EXECUTABLE}")"
echo "   Game assets: $(stat -f "%Sm" "${BUILD_DIR}/${SERVER_PCK}")"

echo ""
echo "✅ Server build complete!"
echo "📁 Output files:"
echo "   - ${BUILD_DIR}/${SERVER_EXECUTABLE} (Main executable)"
echo "   - ${BUILD_DIR}/${SERVER_PCK} (Game assets)" 
echo "   - ${BUILD_DIR}/data_SoreLosers_linuxbsd_x86_64/ (.NET runtime)"
echo ""
echo "🚀 Ready for deployment with deploy_to_aws.sh"
echo ""
echo "🔧 CRITICAL: This build excludes NetworkManager autoload to prevent RPC conflicts"
echo "   Server uses DedicatedServer.cs only, clients use NetworkManager.cs only" 