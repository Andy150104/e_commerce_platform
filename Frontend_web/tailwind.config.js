/** @type {import('tailwindcss').Config} */
export default {
  darkMode: 'class',
  content: [
    './components/**/*.{vue,js,ts}',
    './layouts/**/*.{vue,js,ts}',
    './pages/**/*.{vue,js,ts}',
    './plugins/**/*.{js,ts}',
    './app.vue',
    './node_modules/flowbite/**/*.{js,ts}',
  ],
  theme: {
    extend: {
      screens: {
        '3xl': '1850px',
      },
    },
  },
  plugins: [
    require('flowbite/plugin'),
    require('tailwindcss-animated')
  ],
}
