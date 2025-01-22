<template>
  <div class="relative inline-block text-left ml-96">
    <button
      @click="toggleDropdown"
      class="inline-flex items-center justify-center w-12 h-12 rounded-lg border border-gray-300 bg-white hover:bg-gray-100 dark:bg-gray-800 dark:border-gray-600 dark:hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
    >
      <UIcon :name="currentIcon" class="w-6 h-6 text-gray-600 dark:text-gray-300" />
    </button>
    <div
      v-show="isDropdownOpen"
      @click.away="closeDropdown"
      class="absolute right-0 z-10 mt-2 w-40 bg-white dark:bg-gray-800 rounded-lg shadow-lg border border-gray-300 dark:border-gray-600"
    >
      <ul class="py-1">
        <li
          v-for="icon in icons"
          :key="icon.name"
          @click="selectIcon(icon.name)"
          class="flex items-center space-x-2 px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-700 cursor-pointer"
        >
          <UIcon :name="icon.name" class="w-5 h-5 text-gray-600 dark:text-gray-300" :ref="icon.ref" />
          <span class="text-gray-800 dark:text-gray-300">{{ icon.label }}</span>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref } from 'vue'

  const isDropdownOpen = ref(false)
  const System = ref('1')

  const currentIcon = ref('i-heroicons-desktop-computer')

  const icons = [
    { name: 'i-fluent-desktop-regular', label: 'Desktop', ref: 'System' },
    { name: 'i-heroicons-sun', label: 'Sun', ref: 'Light' },
    { name: 'i-heroicons-moon', label: 'Moon', ref: 'Dark' },
  ]

  const toggleDropdown = () => {
    isDropdownOpen.value = !isDropdownOpen.value
  }

  const closeDropdown = () => {
    isDropdownOpen.value = false
  }

  const selectIcon = (iconName: string) => {
    console.log('System', System.value)
    currentIcon.value = iconName
    closeDropdown()
  }
</script>
