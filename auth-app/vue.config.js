const { defineConfig } = require('@vue/cli-service')
// module.exports = defineConfig({
//   transpileDependencies: true
// })
// module.exports = {
//   outputDir: '../wwwroot',
//   assetsDir: ''
// };
module.exports={
  devServer: {
    https: true, // 啟用 HTTPS
    port: 8080, // 設定前端應用程式的端口
    proxy: 'https://localhost:5001' // 將 API 請求代理到 HTTPS 端點
  }
}
