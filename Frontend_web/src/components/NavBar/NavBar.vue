<template>
  <nav
    class="bg-gradient-to-r from-blue-400 via-blue-500/50 to-blue-400/10 border-b border-gray-200 dark:bg-slate-950 dark:border-gray-600 sticky top-0 z-50 backdrop-blur-lg"
  >
    <!-- Hàng ngang chính (Desktop) -->
    <div class="max-w-screen-xl mx-auto p-4 items-center justify-between md:max-w-screen-xl md:mx-auto md:p-4 flex md:items-center">
      <!-- CỘT TRÁI: Logo -->
      <div class="flex items-center space-x-2 shrink-0">
        <img src="https://flowbite.com/docs/images/logo.svg" alt="Flowbite Logo" class="h-8" />
        <span class="text-2xl font-semibold dark:text-white"> Flowbite </span>
      </div>

      <!-- CỘT GIỮA: Menu (ẩn trên mobile) -->
      <div class="hidden md:flex flex-grow justify-center space-x-8">
        <!-- Home -->
        <a href="/Home" class="text-gray-900 font-semibold dark:text-gray-100 hover:text-blue-600 dark:hover:text-blue-400"> Home </a>
        <!-- About -->
        <a href="#" class="text-gray-900 font-semibold dark:text-gray-100 hover:text-blue-600 dark:hover:text-blue-400"> About </a>
        <!-- Services (dropdown) -->
        <div class="relative">
          <button
            @click.stop="toggleServices"
            class="inline-flex items-center font-semibold text-gray-900 dark:text-gray-100 hover:text-blue-600 dark:hover:text-blue-400"
          >
            Services
            <svg class="w-4 h-4 ml-1" fill="currentColor" viewBox="0 0 20 20">
              <path
                fill-rule="evenodd"
                d="M5.23 7.21a.75.75 0 011.06.02L10 12l3.71-4.77a.75.75 0 111.06 1.06l-4.24 5.44a.75.75 0 01-1.06 0L5.21 8.27a.75.75 0 01.02-1.06z"
                clip-rule="evenodd"
              />
            </svg>
          </button>
          <!-- Dropdown Services -->
          <div
            v-if="isServicesOpen"
            ref="servicesDropdown"
            class="absolute top-full left-0 mt-2 w-40 bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded shadow py-2 z-50"
          >
            <a href="/Service/Buying" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700"> Buying</a>
            <a href="/Service/Exchange" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700"> Exchange </a>
          </div>
        </div>
        <!-- Contact -->
        <a href="#" class="text-gray-900 font-semibold dark:text-gray-100 hover:text-blue-600 dark:hover:text-blue-400"> Contact </a>
      </div>

      <!-- CỘT PHẢI: User hoặc Register/Login (ẩn trên mobile) -->
      <div class="hidden md:flex items-center space-x-4 shrink-0 ml-4">
        <!-- Nếu chưa login => Hiển thị nút Register / Login -->
        <div v-if="!isLogin" class="space-x-2">
          <a href="/Register" class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"> Get Started </a>
          <a href="/Login" class="text-white bg-purple-600 hover:bg-purple-700 focus:ring-4 focus:outline-none focus:ring-purple-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-purple-500 dark:hover:bg-purple-600 dark:focus:ring-purple-800"> Login </a>
        </div>

        <!-- Nếu đã login => Dropdown user -->
        <div v-else class="relative">
          <button
            @click.stop="toggleUser"
            class="inline-flex items-center text-gray-900 dark:text-gray-100 hover:text-blue-600 dark:hover:text-blue-400"
          >
            <img src="https://flowbite.com/docs/images/people/profile-picture-5.jpg" alt="user photo" class="w-8 h-8 rounded-full" />
            <span class="ml-2">Bonnie Green</span>
          </button>
          <!-- Dropdown user -->
          <div
            v-if="isUserOpen"
            ref="userDropdown"
            class="absolute right-0 mt-2 w-48 bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded shadow py-2 z-50"
          >
            <div class="px-4 py-3 border-b border-gray-100 dark:border-gray-600">
              <span class="block text-sm text-gray-900 dark:text-white"> Bonnie Green </span>
              <span class="block text-sm text-gray-500 truncate dark:text-gray-400"> name@flowbite.com </span>
            </div>
            <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700"> Dashboard </a>
            <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700"> Settings </a>
            <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700"> Earnings </a>
            <a
              class="block px-4 py-2 text-sm text-gray-700 text-right hover:bg-gray-100 dark:hover:bg-gray-700 dark:text-gray-200 dark:hover:text-white cursor-pointer"
            >
              Sign out
            </a>
          </div>
        </div>
      </div>

      <!-- Nút toggle (mobile) - đặt cuối cùng để luôn hiển thị -->
      <button
        @click="mobileOpen = !mobileOpen"
        class="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-600 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-300 dark:hover:bg-gray-700 dark:focus:ring-gray-600 ml-2"
      >
        <span class="sr-only">Open main menu</span>
        <svg class="w-5 h-5" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 14" stroke="currentColor" stroke-width="2">
          <path d="M1 1h15M1 7h15M1 13h15" />
        </svg>
      </button>
    </div>

    <!-- MENU MOBILE -->
    <transition name="fade-enter-from">
      <div v-if="mobileOpen" class="md:hidden border-t border-gray-200 dark:border-gray-700">
        <!-- Các link menu dạng dọc -->
        <ul class="flex flex-col font-semibold space-y-1 p-4 bg-white dark:bg-gray-800">
          <li>
            <a href="#" class="block py-2 px-3 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> Home </a>
          </li>
          <li>
            <a href="#" class="block py-2 px-3 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> About </a>
          </li>
          <!-- Dropdown Services trên mobile -->
          <li>
            <button
              @click.stop="servicesMobileOpen = !servicesMobileOpen"
              class="w-full text-left flex items-center justify-between py-2 px-3 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"
            >
              Services
              <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fill-rule="evenodd"
                  d="M5.23 7.21a.75.75 0 011.06.02L10 12l3.71-4.77a.75.75 0 111.06 1.06l-4.24 5.44a.75.75 0 01-1.06 0L5.21 8.27a.75.75 0 01.02-1.06z"
                  clip-rule="evenodd"
                />
              </svg>
            </button>
            <!-- Menu con mobile -->
            <div v-if="servicesMobileOpen" class="ml-6 mt-1 space-y-1">
              <a href="#" class="block px-3 py-1 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> Service 1 </a>
              <a href="#" class="block px-3 py-1 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> Service 2 </a>
              <a href="#" class="block px-3 py-1 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> Service 3 </a>
            </div>
          </li>
          <li>
            <a href="#" class="block py-2 px-3 text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700"> Contact </a>
          </li>
        </ul>

        <!-- Nếu chưa login => Hiển thị nút Register/Login -->
        <div v-if="!isLogin" class="px-4 py-2 flex space-x-2 bg-white dark:bg-gray-800 border-t border-gray-200 dark:border-gray-700">
          <a href="#" class="flex-1 text-center py-2 bg-blue-600 text-white rounded hover:bg-blue-700"> Register </a>
          <a href="#" class="flex-1 text-center py-2 bg-purple-600 text-white rounded hover:bg-purple-700"> Login </a>
        </div>

        <!-- Nếu đã login => Hiển thị user info trên mobile -->
        <div v-else class="p-4 bg-white dark:bg-gray-800 border-t border-gray-200 dark:border-gray-700">
          <div class="flex items-center space-x-2">
            <img src="https://flowbite.com/docs/images/people/profile-picture-5.jpg" alt="User" class="w-8 h-8 rounded-full" />
            <div>
              <p class="text-sm font-semibold dark:text-gray-100">Bonnie Green</p>
              <p class="text-xs text-gray-500 dark:text-gray-400">name@flowbite.com</p>
            </div>
          </div>
          <div class="mt-3 space-y-1">
            <a href="#" class="block px-3 py-2 text-sm text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700">
              Dashboard
            </a>
            <a href="#" class="block px-3 py-2 text-sm text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700">
              Settings
            </a>
            <a href="#" class="block px-3 py-2 text-sm text-gray-700 dark:text-gray-200 rounded hover:bg-gray-100 dark:hover:bg-gray-700">
              Earnings
            </a>
            <a href="#" class="block px-3 py-2 text-sm text-gray-700 dark:text-gray-200 text-right rounded hover:bg-gray-100 dark:hover:bg-gray-700">
              Sign out
            </a>
          </div>
        </div>
      </div>
    </transition>
  </nav>
</template>

<script setup lang="ts">
  import { ref, onMounted, onUnmounted } from 'vue'

  /** Giả lập trạng thái đăng nhập */
  const isLogin = ref(false)

  /** Mở/đóng menu mobile */
  const mobileOpen = ref(false)

  /** Dropdown Services (desktop) */
  const isServicesOpen = ref(false)
  const servicesDropdown = ref<HTMLElement | null>(null)

  /** Dropdown User (desktop) */
  const isUserOpen = ref(false)
  const userDropdown = ref<HTMLElement | null>(null)

  /** Dropdown Services (mobile) */
  const servicesMobileOpen = ref(false)

  /** Khi click ra ngoài => đóng dropdown */
  function handleClickOutside(e: MouseEvent) {
    // Đóng Services
    if (isServicesOpen.value && servicesDropdown.value) {
      if (!servicesDropdown.value.contains(e.target as Node)) {
        isServicesOpen.value = false
      }
    }
    // Đóng User
    if (isUserOpen.value && userDropdown.value) {
      if (!userDropdown.value.contains(e.target as Node)) {
        isUserOpen.value = false
      }
    }
  }

  /** Toggle dropdown desktop */
  function toggleServices() {
    isServicesOpen.value = !isServicesOpen.value
  }
  function toggleUser() {
    isUserOpen.value = !isUserOpen.value
  }

  onMounted(() => {
    window.addEventListener('click', handleClickOutside)
  })
  onUnmounted(() => {
    window.removeEventListener('click', handleClickOutside)
  })
</script>

<style scoped>
  /* Hiệu ứng fade cho menu mobile */
  .fade-enter-active,
  .fade-leave-active {
    transition: opacity 0.2s;
  }
  .fade-enter-from,
  .fade-leave-to {
    opacity: 0;
  }
</style>
