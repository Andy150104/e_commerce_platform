<template>
  <div>
    <button
      type="button"
      class="inline-flex w-8 h-8 p-0 items-center justify-center surface-0 dark:surface-800 border border-surface-200 dark:border-surface-600 rounded"
      @click="toggleDarkMode"
    >
    <i :class="['pi', iconClass, 'dark:text-white', 'text-black']"></i>
    </button>
  </div>
  <Loading />
</template>

<script setup>
import { ref } from 'vue'
import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
import { Theme } from '@PKG_SRC/types/enums/constantFrontend'
import { getThemesSystem, updateThemesStorageAndSetMode } from '@PKG_SRC/utils/functions/ChangeThemes'
import 'primeicons/primeicons.css'

const currentTheme = getThemesSystem()
const isDarkMode = ref(currentTheme === Theme.DARK)
const iconClass = ref(isDarkMode.value ? 'pi-sun' : 'pi-moon')

function toggleDarkMode() {
  const currentTheme = getThemesSystem()
  const isCurrentlyDarkMode = currentTheme === Theme.DARK
  const newTheme = isCurrentlyDarkMode ? Theme.LIGHT : Theme.DARK
  updateThemesStorageAndSetMode(currentTheme, newTheme)
  isDarkMode.value = !isCurrentlyDarkMode
  iconClass.value = isDarkMode.value ? 'pi-sun' : 'pi-moon'
}
</script>
