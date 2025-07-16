# Quick Reference: Card Sizing System

**For developers working with card display in SoreLosers**

---

## 🎯 **Current Card Specifications**

```csharp
// CardGameUI.cs - Lines ~52-53
private readonly Vector2 cardSize = new Vector2(100, 140);        // Hand cards
private readonly Vector2 trickCardSize = new Vector2(100, 140);   // Trick cards
```

**Hand Cards**: 100x140px, overlap by 50px, fan effect  
**Trick Cards**: 100x140px, 20px separation, clear display

---

## ⚠️ **CRITICAL: Don't Just Change cardSize!**

Card sizing in Godot 4.4 requires **5-layer enforcement**:

1. **Texture Config** → `IgnoreTextureSize = true`
2. **Initial Size** → Set `Size` + `CustomMinimumSize` 
3. **Container** → `ClipContents = false`
4. **Post-AddChild** → Re-apply size after adding to scene
5. **Deferred** → `CallDeferred(SetSize)` for final enforcement

---

## 🛠️ **To Modify Card Sizes**

### Change Size:
```csharp
// Modify these constants in CardGameUI.cs:
private readonly Vector2 cardSize = new Vector2(120, 168);        // 20% larger
private readonly Vector2 trickCardSize = new Vector2(120, 168);   // Keep same
```

### Change Spacing:
```csharp
// In UpdatePlayerHand() method:
float overlapSpacing = -60;    // Hand cards (more overlap)

// In UpdateTrickDisplay() method:  
float overlapSpacing = 15;     // Trick cards (less separation)
```

---

## 🔍 **If Cards Look Wrong**

1. **Check debug logs** for actual sizes being applied
2. **Verify all 5 layers** are implemented in card creation
3. **See full guide**: `docs/CARD_SIZING_TECHNICAL_GUIDE.md`

---

## 📁 **Key Files**

- `scripts/CardGameUI.cs` - Main card sizing logic
- `docs/CARD_SIZING_TECHNICAL_GUIDE.md` - Complete technical documentation
- `CHANGELOG_2024_12.md` - Implementation history and rationale

**The sizing system will automatically handle any size changes properly when modified correctly.** 