// https://nuxt.com/docs/api/configuration/nuxt-config
import { resolve } from 'path'

export default defineNuxtConfig({
  devtools: { enabled: false },

  app: {
    head: {
      title: 'Ecomercial',
      meta: [{ name: 'description', content: 'My Nuxt app with SSR enabled' }],
    },
  },

  modules: ['@pinia/nuxt', '@nuxthq/ui', '@pinia/nuxt', 'pinia-plugin-persistedstate/nuxt'],
  ui: {
    global: true,
  },
  css: ['./src/assets/styles/main.css'],

  ssr: false,
  srcDir: 'src/',
  vite: {
    optimizeDeps: {
      include: [],
      exclude: [],
    },
  },

  vue: {
    compilerOptions: {
      isCustomElement: (tag) => ['Placeholder'].includes(tag),
    },
  },

  experimental: {
    renderJsonPayloads: false,
  },

  tailwindcss: {
    configPath: './tailwind.config.js',
    viewer: false,
    quiet: true,
  },

  imports: {
    autoImport: true,
  },

  components: false,

  alias: {
    '@PKG_SRC': resolve(__dirname, './src'),
    "@PKG_API": resolve(__dirname, './api'),
  },
  runtimeConfig: {
    public: {
      apiBaseUrl: process.env['NUXT_PUBLIC_API_BASE_URL'] ?? '',
      identityBaseUrl: process.env['NUXT_PUBLIC_IDENTITY_BASE_URL'] ?? '',
    },
  },
  compatibilityDate: '2025-01-09',
})
