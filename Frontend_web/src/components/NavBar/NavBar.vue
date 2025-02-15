<template>
  <!--  from-blue-900 via-gray-900 to-black  -->
  <nav
    class="bg-gradient-to-r from-blue-400 via-blue-500/50 to-blue-400/10 opacity-80 transition-all duration-500 border-b border-gray-200 dark:bg-slate-950 dark:border-gray-600 dark:opacity-90 sticky top-0 z-50 backdrop-blur-lg"
  >
    <div class="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
      <a href="https://flowbite.com/" class="flex items-center space-x-3 rtl:space-x-reverse">
        <img src="https://flowbite.com/docs/images/logo.svg" class="h-8" alt="Flowbite Logo" />
        <span class="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">Flowbite</span>
      </a>
      <div v-show="isLogin" class="relative md:order-2">
        <button
          @click="toggleDropdown"
          class="flex items-center text-sm font-medium text-gray-900 rounded-full dark:text-white focus:ring-4 focus:ring-gray-300 dark:focus:ring-gray-600"
        >
          <img class="w-8 h-8 rounded-full" src="https://flowbite.com/docs/images/people/profile-picture-5.jpg" alt="user photo" />
          <span class="ml-2 text-sm text-gray-900 dark:text-white">Bonnie Green</span>
        </button>

        <!-- Dropdown -->
        <div v-show="isDropdownOpen" class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg dark:bg-gray-700">
          <div class="px-4 py-3 border-b border-gray-100 dark:border-gray-600">
            <span class="block text-sm text-gray-900 dark:text-white">Bonnie Green</span>
            <span class="block text-sm text-gray-500 truncate dark:text-gray-400">name@flowbite.com</span>
          </div>
          <ul class="py-2">
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Dashboard</a
              >
            </li>
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Settings</a
              >
            </li>
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Earnings</a
              >
            </li>
            <li>
              <a
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white cursor-pointer"
                >Sign out</a
              >
            </li>
          </ul>
        </div>
      </div>
      <div v-show="isLogin" class="flex md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse gap-2">
        <a
          v-if="!isLogin"
          href="/Register"
          type="button"
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          Get started
        </a>
        <a
          v-if="!isLogin"
          href="/Login"
          class="text-white bg-purple-600 hover:bg-purple-700 focus:ring-4 focus:outline-none focus:ring-purple-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-purple-500 dark:hover:bg-purple-600 dark:focus:ring-purple-800"
        >
          Login
        </a>
        <button
          data-collapse-toggle="navbar-cta"
          type="button"
          class="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
          aria-controls="navbar-cta"
          aria-expanded="false"
        >
          <span class="sr-only">Open main menu</span>
          <svg class="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 14">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 1h15M1 7h15M1 13h15" />
          </svg>
        </button>
      </div>
      <div class="items-center justify-between hidden w-full md:flex md:w-auto md:order-1" id="navbar-cta">
        <ul
          :class="[
            'flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg md:space-x-8 rtl:space-x-reverse md:flex-row md:mt-0 md:border-0 dark:border-gray-700',
            isMobile ? ' bg-gray-50 md:bg-white dark:bg-gray-800 md:dark:bg-slate-950' : '',
          ]"
        >
          <li>
            <a
              href="#"
              class="block py-2 px-3 md:p-0 text-white bg-blue-700 rounded md:bg-transparent md:text-blue-700 md:dark:text-blue-500"
              aria-current="page"
              >Home</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 md:p-0 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >About</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 md:p-0 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >Services</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 md:p-0 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >Contact</a
            >
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
<script setup lang="ts">
  import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
  import { initDropdowns, initFlowbite } from 'flowbite'
  import { onMounted } from 'vue'

  const isMobile = ref(false)
  const authStore = useAuthStore()
  const isLogin = ref(false)
  const router = useRouter()
  const isDropdownOpen = ref(false)

  const toggleDropdown = () => {
    isDropdownOpen.value = !isDropdownOpen.value
  }

  const handleResize = () => {
    if (window.innerWidth < 768) {
      isMobile.value = true
    } else {
      isMobile.value = false
    }
  }

  onMounted(() => {
    window.addEventListener('resize', handleResize)
    handleResize()
    initFlowbite()
    initDropdowns()
    isLogin.value = true
  })

  onUnmounted(() => {
    window.removeEventListener('resize', handleResize)
  })
</script>
