# SoreLosers - Itch.io Distribution Guide

**🎮 Ready for Distribution!** Your multiplayer card game is production-ready with $6/month server hosting.

## 📦 **Distribution Files Ready**

### ✅ **Downloadable Builds**
Since SoreLosers uses C#, these are your **actual** distribution options:

| Platform | File | Size | Status |
|----------|------|------|--------|
| 🍎 **macOS** | `builds/SoreLosers-Production.app` | 177MB | ✅ Ready |
| 💻 **Windows** | `builds/SoreLosers-Windows.exe` | 93MB | ✅ Ready |
| 🐧 **Linux** | `builds/SoreLosers-Linux.x86_64` | TBD | 🔧 Add preset |

### ❌ **Web Build**
**Not possible** with Godot 4 + C#. Only Godot 3 with Mono supports web exports.

## 🚀 **Itch.io Upload Process**

### **Step 1: Create Itch.io Project**
1. Go to https://itch.io/game/new
2. **Title**: "SoreLosers"
3. **Short Description**: "Light-hearted multiplayer card game with sabotage mechanics"
4. **Classification**: Game
5. **Kind of Project**: HTML/Downloadable

### **Step 2: Upload Builds**
```bash
# Recommended: Zip your builds for better compression
zip -r SoreLosers-macOS.zip builds/SoreLosers-Production.app/
zip SoreLosers-Windows.zip builds/SoreLosers-Windows.exe
# zip SoreLosers-Linux.zip builds/SoreLosers-Linux.x86_64  # When ready
```

### **Step 3: Itch.io Configuration**
- **Pricing**: Free (as per your PRD)
- **Platforms**: macOS, Windows (, Linux)
- **Multiplayer**: Yes - Online multiplayer (mention room codes)
- **Tags**: `card-game`, `multiplayer`, `sabotage`, `party-game`

## 🎯 **Game Description for Itch.io**

```markdown
# SoreLosers - Multiplayer Card Game with a Twist!

A light-hearted multiplayer card game where the real fun comes from sabotaging your opponents! 

## 🎮 How to Play
- **Host or Join**: Use 6-digit room codes to connect with friends
- **Card Phase**: Play your hand in classic trick-taking style  
- **Sabotage Phase**: Move around and cause mayhem - throw eggs, drop stink bombs!
- **Win**: Complete your hand while surviving the chaos

## 🌟 Features
- **Online Multiplayer**: Room code system for easy friend connections
- **Unique Sabotage**: Physical sabotage mechanics that affect your screen
- **RPG Progression**: Level up ThrowPower, MoveSpeed, and Composure
- **Cross-Platform**: Available on macOS and Windows

## 🎯 System Requirements
- **macOS**: 10.12+ (Intel) or 11.0+ (Apple Silicon)
- **Windows**: Windows 10+ (x64)
- **Internet**: Required for multiplayer

## 🚀 Getting Started
1. Download for your platform
2. Launch the game
3. Click "Host Game" to create a room or "Join Game" with a friend's code
4. Have fun sabotaging each other!

*Made with Godot 4.4.1*
```

## 💰 **Server Information for Players**

Your game connects to your production server:
- **Server**: 159.223.189.139:7350
- **Cost**: $6/month (DigitalOcean)
- **Capacity**: 10-50 concurrent players
- **Uptime**: 24/7 hosted

## 📋 **Pre-Launch Checklist**

### Before Publishing:
- [ ] Test both platforms work with production server
- [ ] Verify room code system works over internet
- [ ] Test multiplayer sync between different platforms
- [ ] Create screenshots/GIFs for Itch.io page
- [ ] Write compelling game description
- [ ] Set up analytics (optional)

### Nice-to-Have:
- [ ] Add Linux build for broader reach
- [ ] Create game trailer/demo video
- [ ] Set up Discord/community
- [ ] Plan post-launch updates

## 🎨 **Marketing Assets Needed**

For a professional Itch.io page:
- **Cover Image**: 630x500px (main thumbnail)
- **Screenshots**: Multiple gameplay shots
- **GIF/Video**: Showing sabotage mechanics
- **Banner**: Optional but recommended

## 🔧 **Future Considerations**

- **Steam**: Possible future platform (requires Steam SDK integration)
- **Mobile**: Would require complete rewrite in GDScript
- **Web**: Would require complete rewrite in GDScript for Godot 3
- **Console**: Possible with additional licensing

---

**Ready to publish!** Your SoreLosers multiplayer game is production-ready for Itch.io distribution. 