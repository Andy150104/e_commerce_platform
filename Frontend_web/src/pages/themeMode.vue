<script setup>
  import { ref } from 'vue'

  const isDarkMode = ref(false)
  const refThumb = ref(null)

  const toggleDarkMode = async (darkMode) => {
    if (!refThumb.value || !document.startViewTransition || window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
      isDarkMode.value = darkMode
      document.documentElement.classList.toggle('dark', darkMode)
      return
    }

    // Lấy vị trí và bán kính cho hiệu ứng tròn
    const { top, left, width, height } = refThumb.value.getBoundingClientRect()
    const x = left + width / 2
    const y = top + height / 2
    const right = window.innerWidth - left
    const bottom = window.innerHeight - top
    const maxRadius = Math.hypot(Math.max(left, right), Math.max(top, bottom))

    // Thực hiện hiệu ứng View Transition
    await document.startViewTransition(() => {
      isDarkMode.value = darkMode
    }).ready

    // Áp dụng hiệu ứng clipPath
    const animation = document.documentElement.animate(
      {
        clipPath: darkMode
          ? [`circle(0px at ${x}px ${y}px)`, `circle(${maxRadius}px at ${x}px ${y}px)`]
          : [`circle(${maxRadius}px at ${x}px ${y}px)`, `circle(0px at ${x}px ${y}px)`],
      },
      {
        duration: 1000,
        easing: 'ease-in-out',
      }
    )

    // Chờ hiệu ứng kết thúc trước khi thay đổi class
    await animation.finished
    document.documentElement.classList.toggle('dark', darkMode)
  }
</script>

<template>
  <div class="h-screen w-screen flex items-center justify-center bg-white dark:bg-gray-950">
    <div class="relative inline-flex items-center">
      <button
        class="relative w-12 h-6 bg-gray-300 dark:bg-gray-600 rounded-full transition-all"
        :aria-checked="isDarkMode"
        role="switch"
        @click="toggleDarkMode(!isDarkMode)"
      >
        <div
          ref="refThumb"
          class="absolute top-1 left-1 w-4 h-4 bg-white dark:bg-black rounded-full transition-transform"
          :class="{ 'translate-x-6': isDarkMode }"
        >
          <span v-if="isDarkMode" class="hidden">🌙</span>
          <span v-else class="hidden">☀️</span>
        </div>
      </button>
    </div>
  </div>
</template>

<style scoped>
  /* Đảm bảo hiệu ứng View Transition được tắt mặc định */
  ::view-transition-old(root),
  ::view-transition-new(root) {
    animation: none;
    mix-blend-mode: normal;
  }
</style>
