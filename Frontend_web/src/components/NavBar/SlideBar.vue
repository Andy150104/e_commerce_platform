<template>
  <!-- drawer init and show -->
  <div class="text-center">
    <slot name="button_ui"></slot>
  </div>

  <!-- drawer component -->
  <div
    id="drawer-navigation"
    :class="'fixed z-40 w-[500px] h-screen p-4 overflow-y-auto transition-transfor -translate-x-full bg-white dark:bg-gray-800 top-0 left-0'"
    tabindex="-1"
    aria-labelledby="drawer-navigation-label"
  >
    <h2 class="text-lg font-semibold mb-4">{{ titleSideBar }}</h2>
    <slot name="body"></slot>
  </div>
</template>
<script setup lang="ts">
  import { initDrawers, initFlowbite } from 'flowbite'

  const props = defineProps({
    titleSideBar: {
      type: String,
      required: false,
      default: 'FILTERS',
    },
  })

  onMounted(() => {
    initFlowbite()
    initDrawers()
  })
  const filters = ref({
    brand: 'Apple',
    model: 'iMac 27',
    priceFrom: 300,
    priceTo: 3500,
    condition: 'All',
    colours: ['Green', 'Red'],
    rating: 3,
    delivery: 'Asia',
  })

  const colors = ref([
    { name: 'Blue', hex: '#007BFF' },
    { name: 'Gray', hex: '#6C757D' },
    { name: 'Green', hex: '#28A745' },
    { name: 'Pink', hex: '#E83E8C' },
    { name: 'Red', hex: '#DC3545' },
  ])

  const deliveryRegions = ref(['USA', 'Europe', 'Asia', 'Australia'])

  const renderStars = (count: number) => {
    return '★'.repeat(count) + '☆'.repeat(5 - count)
  }

  const clearFilters = () => {
    filters.value = {
      brand: 'Apple',
      model: 'iMac 27',
      priceFrom: 300,
      priceTo: 3500,
      condition: 'All',
      colours: [],
      rating: 0,
      delivery: 'Asia',
    }
  }
</script>
