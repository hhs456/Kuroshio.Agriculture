### Namespace: `Kuroshio.Agriculture`
此命名空間包含有關屯田系統的系統程式碼，包含農田和種子資料的結構，並處理農田狀態的更新與種植邏輯。

---

### Class: `FarmData`
屯田資料的儲存和處理，包括種植狀態、肥沃度等資訊。
- **Properties**:
  - `SeedId`: 種子的唯一識別碼
  - `IsPlanted`: 是否種植的狀態
  - `Fertility`: 農田的肥沃度
  - `PlantTime`: 種植的時間
  - `ValidTime`: 種植有效期
  - `RipenTime`: 種子的成熟時間
  - `ConsumptionRate`: 肥沃度消耗率

- **Methods**:
  - `SetSeedArgs(SeedData data)`: 接收 `SeedData` 並設置種子的相關參數。

---

### Struct: `SeedData`
種子的基礎數據結構。
- **Properties**:
  - `SeedId`: 種子的唯一ID
  - `RipenTime`: 成熟所需時間
  - `ConsumptionRate`: 消耗肥沃度的比率

- **Constructor**:
  - `SeedData(int seedId, TimeSpan ripenTime, float consumptionRate)`: 用於初始化種子資料。

---

### Interface: `ISeedArgs`
定義種子資料結構的共通屬性。
- **Properties**:
  - `ConsumptionRate`: 肥沃度消耗率
  - `RipenTime`: 種子的成熟時間