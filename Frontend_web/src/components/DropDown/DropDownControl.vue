<template>
  <div>
    <button
      type="button"
      class="flex items-center w-full p-2 text-base text-gray-900 transition duration-75 rounded-lg group hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
      :aria-controls="'dropdown-' + itemName"
      :data-collapse-toggle="'dropdown-' + itemName"
    >
      <slot name="body"></slot>
      <span class="flex-1 ms-16 text-left rtl:text-right whitespace-nowrap">{{ itemName }}</span>
      <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 4 4 4-4" />
      </svg>
    </button>
    <ul :id="'dropdown-' + itemName" :class="['py-2 space-y-2 transition-all duration-200 ease-in-out', isOpen ? 'block' : 'hidden']">
      <li v-for="option in option">
        <NuxtLink
          :to="option.link"
          exact-active-class="bg-blue-100 text-blue-900 dark:bg-blue-700 dark:text-blue-100 border-l-4 border-blue-500"
          class="flex items-center w-full p-2 text-gray-900 transition duration-75 rounded-lg pl-16 group hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
          >{{ option.childItemName }}</NuxtLink
        >
      </li>
    </ul>
  </div>
</template>
<script setup lang="ts">
  interface DropDownItem {
    link: string
    childItemName: string
  }
  const props = defineProps({
    itemName: {
      type: String,
      required: true,
      default: '',
    },
    option: {
      type: Array as PropType<DropDownItem[]>,
      required: true,
      default: () => [],
    },
  })
  const route = useRoute()
  const isOpen = ref(false)
  onMounted(() => {
    const isActiveChild = props.option.some((child) => route.path.includes(child.link))
    if (isActiveChild) {
      isOpen.value = true
    }
  })
</script>
