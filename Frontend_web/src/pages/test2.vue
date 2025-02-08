<template>
  <div>
    <button @click="toggleDarkMode" class="p-2 rounded bg-gray-800 text-white">
      {{ isDarkMode ? 'Light Mode' : 'Dark Mode' }}
    </button>
    <div class="p-4 bg-white dark:bg-gray-800 dark:text-white">
      <p>Chế độ: {{ isDarkMode ? 'Dark' : 'Light' }}</p>
    </div>
  </div>
  <Loading />
</template>

<script setup>
  import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
  import { Theme } from '@PKG_SRC/types/enums/constantFrontend'
  import { getThemesSystem, updateThemesStorageAndSetMode } from '@PKG_SRC/utils/functions/ChangeThemes'
  import { ref } from 'vue'

  const isDarkMode = ref(false)

  function toggleDarkMode() {
    const currentTheme = getThemesSystem()
    const isCurrentlyDarkMode = currentTheme === Theme.DARK
    const newTheme = isCurrentlyDarkMode ? Theme.LIGHT : Theme.DARK
    if (isDarkMode) return updateThemesStorageAndSetMode(currentTheme, newTheme)
  }
</script>
