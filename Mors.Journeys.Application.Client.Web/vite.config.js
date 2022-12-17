import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig(env => ({
  base: env.command == "build" ? "/sites/journeys/" : "",
  define: { "__API_URL__": JSON.stringify(env.command == "build" ? "http://192.168.0.213:65363" : "http://localhost:65363") },
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
      '~bootstrap': fileURLToPath(new URL('./node_modules/bootstrap', import.meta.url)),
    }
  }
})
)
