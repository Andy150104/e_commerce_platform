<template>
  <!-- <div>
    <NuxtRouteAnnouncer />
    <NuxtWelcome />
  </div> -->
  <NuxtLayout>
    <NuxtPage />
  </NuxtLayout>
</template>
<script lang="ts" setup>
  import { onMounted, ref } from 'vue'
  import { getThemesSystem, updateThemesStorageAndSetMode } from './utils/functions/ChangeThemes'
  import { Theme } from './types/enums/constantFrontend'

  const isDarkMode = ref(false)

  onMounted(() => {
    const currentTheme = getThemesSystem()
    if (currentTheme) {
      updateThemesStorageAndSetMode(currentTheme, currentTheme)
      isDarkMode.value = currentTheme === Theme.DARK
    }
  })
  window.addEventListener('storage', (event) => {
    if (event.key === 'nuxt-color-mode') {
      const newTheme = event.newValue
      if (newTheme) {
        updateThemesStorageAndSetMode(newTheme, newTheme)
        isDarkMode.value = newTheme === Theme.DARK
      }
    }
  })
</script>
<style scoped>
  html.\30 .dark {
    background: red;
  }
</style>
