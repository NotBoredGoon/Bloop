<p align="center">
  <img src="docs/assets/logo.png" width="180" alt="Bloop logo" />
</p>

<h1 align="center"><strong>Bloop</strong></h1>

<p align="center">
  A bite-size, minimalistic 2-D platformer packed with hidden Easter eggs.<br>
  Collect them all, unlock the secret level, and hop through portals to reach the finish!
</p>

---

## 🎮 Play it

| Platform | Build | Link |
|----------|-------|------|
| Universal | v1.0 (stable) | **[Download Bloop](<GOOGLE-DRIVE-LINK>)** |

> **Windows:** unzip and double-click `Bloop.exe`.  
> **macOS / Linux:** run the launch script in the root folder.

If the download doesn’t work, clone this repo and build the game in **Unity 2D**. Quick video guide → <https://youtu.be/7nxKAtxGSn8?t=292>.  
*Remember to select every scene in **Build Settings → Scenes In Build** before pressing **Build**.*

## 🖼️ Screenshots
<p align="center">
  <img src="docs/assets/screen-01.jpg" width="45%" alt="Level 1" />
  <img src="docs/assets/screen-02.png" width="45%" alt="Level Menu" />
</p>

## 🗺️ How to Play

| Action        | Keys |
|---------------|------|
| **Move**      | `←` / `→` **or** `A` / `D` |
| **Jump**      | `Space` |
| **Restart**   | `Esc` → **Restart** |
| **Goal**      | Reach the portal to advance.<br>Grab every Easter egg to unlock the secret level! |

## 🔄 Resetting Progress

Developers & speed-runners can wipe saves for rapid testing:

1. Open **`Scripts/GameUtil.cs`**  
2. *Comment out* the two lines marked **RESET-OFF**  
3. *Uncomment* the three lines marked **RESET-ON**  
4. Play Level 1 — the game will auto-skip to the last level, clearing the save.  
5. Revert the changes when you’re done.

## 🛠️ Build & Contribute
```bash
git clone https://github.com/NotBoredGoon/Bloop.git
cd Bloop
# Open the project in Unity 2022 LTS (2D template)
# Run the game or build via File → Build Settings
