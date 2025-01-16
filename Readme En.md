# GeppakuLab Random System Project

🌙 **GeppakuLab Random System** is a random number generator program developed as a special gift for the Virtual YouTuber **月白 累 (Geppaku Lui)**.

## 🎯 Features
- **Random Number Generation:** Customize the range and quantity of numbers to be generated.
- **Sorting Options:** Sort results in ascending or descending order.
- **Duplicate Control:** Enable or disable duplicate results.
- **Minimum Resolution Lock:** Prevent resizing the window below a defined minimum resolution.

---

## 📽️ Demonstration Videos

### 🎥 **Random Number Generation Showcase**
[![GeppakuLab ランダム·システム Showcase](https://img.youtube.com/vi/j1sl5BKaaOg/0.jpg)](https://youtu.be/j1sl5BKaaOg)

### 🎥 **Settings and Options Showcase**
[![Settings Showcase](https://img.youtube.com/vi/RKB7lpRkYCU/0.jpg)](https://youtu.be/RKB7lpRkYCU)

---

## 📦 Installation
1. Download the latest version from the [Releases](https://github.com/dev-SLH/GeppakuLabRandomSystem/releases) section.
2. Run the provided `GeppakuLab_RandomSystem_Installer.exe` file.
3. Follow the on-screen instructions to complete the installation.

---

## 🛠️ System Requirements
- **Operating System:** Windows 10 or later
- **Runtime:** .NET Framework 4.7.2 or higher

---

## 📖 How to Use

1. **Set Range:** Specify the minimum and maximum values for the random numbers.
2. **Set Quantity:** Choose the number of random values to generate.
3. **Generate:** Click the `Generate` button to create random numbers.
4. **Sort and Resort:** Use the dropdown menu to select sorting options (ascending or descending).
5. **Copy Results:** Copy the generated numbers to the clipboard.

---

## 🛡️ License
This project is licensed under the **MIT License**. For details, see the `LICENSE` file.

---

## 💌 Credits
- **Developer:** [Seol Ryeonghwa (雪霊花) - SLH](https://x.com/slh3951)
- **Developed for:** [Geppaku Lui (月白 累)](https://www.youtube.com/@Geppaku_Lui)

---

## 🚀 Getting Started with the Unity Project

### 1. Import Required Assets
1. Purchase and download the [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) from the Unity Asset Store.
2. Import the Modern UI Pack into your Unity project.

### 2. Apply Presets
1. In the Unity Editor, navigate to **Tools > Modern UI Pack > Apply UIManager Preset**.
2. **If presets are not applied automatically:**
   - Navigate to `Assets/Modern UI Pack/Resources/`, select `MUIP Manager.asset`,
     and drag the `Preset` file into the **Inspector**.

### 3. Run the Sample Scene
1. Open the provided sample scene (`Assets/Scenes/GeppakuLabRandomSystem.unity`).
2. Press the **Play** button in the Unity Editor to run the project.

---

## ⚠️ Notes for Using the Unity Project

### 📌 Check Required Assets
- Ensure all necessary packages and assets are imported.
- Required asset: [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) (purchase required).

### 📌 Unity Version Compatibility
- Recommended Unity version: **6000.0.32f1 or later**.
- Older versions may not support all features properly.

### 📌 Scene Configuration
- Use the provided sample scene as is.
- ⚠️ **Caution:** Modifying scene settings may cause unexpected behavior.

### 📌 Plugin Settings
- Do not modify the settings of plugins used in the project.

### 📌 Save Regularly
- To prevent data loss, save your project regularly.

---

## 📦 About the Modern UI Pack

This project utilizes the [**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) from the Unity Asset Store.

### ✅ **How to Apply the UIManager Preset**

If the **MUIP Manager** preset is not applied automatically, follow these steps:

1. **Unity Editor Menu:**
   - Navigate to `Tools > Modern UI Pack > Apply UIManager Preset`.
2. **Automatic Application:**
   - The `UIManager` preset will be applied to the `MUIP Manager` ScriptableObject.
3. **Confirm Success:**
   - Check the console for the message: `Successfully applied 'UIManager' to 'MUIP Manager'`.

### 🛠️ **Manual Application:**

- If the preset is not applied automatically:
  1. Navigate to `Assets/Modern UI Pack/Resources/`.
  2. Select `MUIP Manager.asset` and manually drag the `Preset` file into the **Inspector**.

### 🚀 **Additional Information:**

- Due to `.gitignore` settings, only `.meta` files are provided for paid assets.
- To run the project smoothly, purchase and import the [**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717).

---

## 📝 Project Structure
```plaintext
Assets/
├── Scripts/                 # Core logic for the random system
├── UIManager                # Preset files for UI consistency
└── Scenes/                  # Sample scenes
```

---

## 🛠️ Development Notes
### Key Challenges and Solutions
- **Button Spamming Issue:** Implemented debouncing logic for responsive UI interactions.
- **UI Reset Logic:** Enhanced the `ClearScrollArea` method to selectively clear active objects.
- **Preset Automation:** Automated the application of UI presets for consistency in shared projects.

---

## 💬 Contributing
Feel free to fork the repository and create pull requests for new features or bug fixes. For major changes, please open an issue first to discuss your ideas.

---

## 📝 License
This project is licensed under the MIT License. For details, see the `LICENSE` file.

---

## 📞 Contact
For inquiries, please contact: [slh3951@gmail.com]

---

**Thank you for using GeppakuLab Random System!** 🌙

