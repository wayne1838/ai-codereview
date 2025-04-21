🚀 Code Review with Grok
Code Review with Grok 是一個 GitHub Action，可自動化執行 AI 程式碼審查。它使用 x.ai Grok 提供的 API，自動讀取 Pull Request 的差異（diff），並提供具建設性的程式碼建議與意見。

這個工具幫助開發團隊節省時間、提升程式碼品質，是現代開發流程中不可或缺的助手。

🔍 功能特色
✅ 自動審查每次 PR 開啟或更新時的程式碼差異。

🧠 使用 Grok AI 模型分析程式碼，提出建議與改善方向。

💬 直接在 PR 留言回饋意見。

⚡ 輕量快速，無需安裝額外依賴。

🔐 基於 GitHub Token 與 Grok API Key 的安全認證機制。

🛠️ 安裝與設定
1. 建立 Secret
請在 GitHub 專案的 Settings > Secrets and variables > Actions 中加入以下 Secrets：

GROK_API_KEY：你從 x.ai 取得的 Grok API 金鑰

GITHUB_TOKEN：GitHub 自動提供，無需手動新增，但仍需在 workflow 中使用

2. 新增 GitHub Action Workflow
在你的 repo 中建立 .github/workflows/main.yml 檔案，內容如下：

yaml
複製
編輯
name: Code Review with Grok

on:
  pull_request:
    types: [opened, synchronize]

permissions:
  pull-requests: write

jobs:
  review:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v3

      - name: Grok Review and Comment
        run: |
          echo "${{ secrets.GITHUB_TOKEN }}" | gh auth login --with-token

          DIFF=$(gh pr diff ${{ github.event.pull_request.number }})
          SAFE_CONTENT=$(jq -Rn --arg text "$DIFF" '{"model": "grok-3-mini-beta", "messages": [{"role": "user", "content": $text}]}')

          RESPONSE=$(curl -s -X POST https://api.x.ai/v1/chat/completions \
            -H "Authorization: Bearer ${{ secrets.GROK_API_KEY }}" \
            -H "Content-Type: application/json" \
            -d "$SAFE_CONTENT")

          COMMENT=$(echo "$RESPONSE" | jq -r '.choices[0].message.content')
          gh pr comment ${{ github.event.pull_request.number }} --body "$COMMENT"
⚙️ 運作原理
偵測 PR 活動：當 PR 被建立或更新時觸發。

取得差異檔 (diff)：使用 gh pr diff 抓取變動程式碼。

呼叫 Grok API：將 diff 資料以 Chat Completion 格式送出。

接收 AI 建議：由 Grok 回傳具體的審查意見。

留言至 PR：透過 gh pr comment 自動將評論貼至對應 PR。
