import { Theme } from '@PKG_SRC/types/enums/constantFrontend'
import { className } from '../class/className'

export function getThemesSystem() {
  return localStorage.getItem('nuxt-color-mode')
}

export function updateThemesStorageAndSetMode(currentThemeSystem: string, newTheme: string) {
  const html = document.documentElement

  if ([Theme.DARK, Theme.LIGHT, Theme.SYSTEM].includes(currentThemeSystem as Theme)) {
    localStorage.setItem('nuxt-color-mode', newTheme)
  }

  switch (newTheme) {
    case Theme.LIGHT:
      updateHtmlClass(html, className.LIGHT_THEME, className.DARK_THEME)
      break
    case Theme.DARK:
      updateHtmlClass(html, className.DARK_THEME, className.LIGHT_THEME)
      break
    case Theme.SYSTEM:
      checkAndUpdateSystemTheme(currentThemeSystem)
      break
  }
}
export function convertThemeToClass(currentThemeSystem: string) {
  const html = document.documentElement
  switch (currentThemeSystem) {
    case Theme.LIGHT:
      return className.LIGHT_THEME
    case Theme.DARK:
      return className.DARK_THEME
    case Theme.SYSTEM:
      return className.SYSTEM_THEME
  }
}
export function checkAndUpdateSystemTheme(currentThemeSystem: string) {
  if (currentThemeSystem !== Theme.SYSTEM) return

  const hour = new Date().getHours()
  const isDarkMode = hour >= 18 || hour < 6

  updateThemesStorageAndSetMode(currentThemeSystem, isDarkMode ? Theme.DARK : Theme.LIGHT)
}

function updateHtmlClass(html: HTMLElement, addClass: string, removeClass: string) {
  html.classList.add(addClass)
  html.classList.remove(removeClass)
}
