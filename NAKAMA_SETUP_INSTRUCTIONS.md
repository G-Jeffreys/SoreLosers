# Nakama Client Setup Instructions

## Step 1: Download Nakama Godot Client

1. **Download the Nakama Godot client from GitHub:**
   - Go to: https://github.com/heroiclabs/nakama-godot
   - Download the latest release or clone the repository

2. **Add to your project:**
   ```bash
   # Option A: Download release and extract
   # Copy the 'addons/nakama/' folder to your project's 'addons/' directory
   
   # Option B: Clone directly (recommended)
   cd SoreLosers
   git submodule add https://github.com/heroiclabs/nakama-godot.git addons/nakama-temp
   cp -r addons/nakama-temp/addons/nakama addons/
   rm -rf addons/nakama-temp
   ```

3. **Enable the plugin in Godot:**
   - Open your project in Godot
   - Go to Project → Project Settings → Plugins
   - Find "Nakama" in the list and enable it
   - Click "Build" if prompted to build C# assemblies

## Step 2: Start Nakama Server

```bash
# In your project root directory:
docker-compose up -d

# Verify it's running:
curl http://localhost:7350/
# Should return: {"server_time": "..."}
```

## Step 3: Test Connection

Once you've added the Nakama client and built the project, the NakamaManager.cs should compile without errors.

## Project Structure After Setup

```
SoreLosers/
├── addons/
│   └── nakama/           # <-- Nakama Godot client
│       ├── plugin.cfg
│       ├── project.godot
│       └── src/          # C# client library
├── scripts/
│   ├── NakamaManager.cs  # ✅ Already created
│   └── MatchManager.cs   # 🔜 Next step
└── docker-compose.yml    # ✅ Already created
```

## Troubleshooting

### If you get "Nakama namespace not found" errors:
1. Make sure the plugin is enabled in Project Settings → Plugins
2. Try "Build" → "Build Project" in Godot
3. Restart Godot and rebuild

### If Docker containers won't start:
```bash
# Check if ports are in use:
netstat -an | grep 7350

# Stop any conflicting services:
docker-compose down
docker-compose up -d
```

## Next Steps

Once setup is complete, proceed with:
1. Creating MatchManager.cs for game state handling
2. Updating MainMenuUI.cs to use Nakama instead of AWS
3. Testing basic authentication and match creation 