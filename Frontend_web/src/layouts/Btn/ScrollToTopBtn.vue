<template>
  <button
    type="button"
    class="!fixed bottom-5 end-5 hidden rounded-full bg-blue-500 p-5 text-xs font-medium uppercase leading-tight text-white shadow-md transition duration-150 ease-in-out hover:bg-teal-700 hover:shadow-lg focus:bg-teal-400 focus:shadow-lg focus:outline-none focus:ring-0 active:bg-teal-800 active:shadow-lg"
    id="btn-back-to-top"
    @click="backToTop"
    ref="backToTopButton"
  >
    <span class="[&>svg]:w-4">
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="3" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" d="M4.5 10.5 12 3m0 0 7.5 7.5M12 3v18" />
      </svg>
    </span>
  </button>
</template>

<script lang="ts" setup>
  import { onMounted, ref } from 'vue'

  const backToTopButton = ref<HTMLButtonElement | null>(null)

  const scrollFunction = () => {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      backToTopButton.value?.classList.remove('hidden')
    } else {
      backToTopButton.value?.classList.add('hidden')
    }
  }

  const backToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  onMounted(() => {
    window.addEventListener('scroll', scrollFunction)
  })
</script>
