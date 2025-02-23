<template>
  <!-- drawer init and show -->
  <div class="text-center">
    <button
      class="flex items-center gap-2 text-gray-900 bg-white border border-gray-300 hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-sm px-5 py-2.5 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
      type="button"
      data-drawer-target="drawer-navigation"
      data-drawer-show="drawer-navigation"
      aria-controls="drawer-navigation"
    >
      <slot name="button_ui"></slot>
    </button>
  </div>

  <!-- drawer component -->
  <div
    id="drawer-navigation"
    class="fixed top-0 left-0 z-40 w-[500px] h-screen p-4 overflow-y-auto transition-transform -translate-x-full bg-white dark:bg-gray-800"
    tabindex="-1"
    aria-labelledby="drawer-navigation-label"
  >
    <h2 class="text-lg font-semibold mb-4">FILTERS</h2>

    <!-- Product Brand & Model -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Product Brand</label>
      <select v-model="filters.brand" class="w-full p-2 border rounded-md">
        <option value="Apple">Apple</option>
        <option value="Samsung">Samsung</option>
      </select>
    </div>

    <div class="mb-4">
      <label class="block text-sm font-medium">Product Model</label>
      <select v-model="filters.model" class="w-full p-2 border rounded-md">
        <option value="iMac 27">iMac 27"</option>
        <option value="MacBook Pro">MacBook Pro</option>
      </select>
    </div>

    <!-- Manufacture Year -->
    <div class="mb-4 flex gap-2">
      <input type="date" class="w-full p-2 border rounded-md" />
      <input type="date" class="w-full p-2 border rounded-md" />
    </div>

    <!-- Price Range -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Price Range</label>
      <input type="range" min="0" max="5000" v-model="filters.priceFrom" class="w-full" />
      <div class="flex justify-between text-sm">
        <input v-model="filters.priceFrom" class="w-20 p-1 border rounded-md" />
        <input v-model="filters.priceTo" class="w-20 p-1 border rounded-md" />
      </div>
    </div>

    <!-- Condition -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Condition</label>
      <div class="flex gap-2">
        <input type="radio" v-model="filters.condition" value="All" /> All <input type="radio" v-model="filters.condition" value="New" /> New
        <input type="radio" v-model="filters.condition" value="Used" /> Used
      </div>
    </div>

    <!-- Colour Selection -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Colour</label>
      <div class="flex flex-wrap gap-2">
        <label v-for="color in colors" :key="color.name" class="flex items-center gap-1">
          <input type="checkbox" v-model="filters.colours" :value="color.name" />
          <span :style="{ backgroundColor: color.hex }" class="w-4 h-4 rounded-full border"></span>
          {{ color.name }}
        </label>
      </div>
    </div>

    <!-- Rating -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Rating</label>
      <div class="flex flex-col gap-1">
        <label v-for="rating in 5" :key="rating" class="flex items-center gap-1">
          <input type="radio" v-model="filters.rating" :value="rating" />
          <span v-html="renderStars(rating)"></span>
        </label>
      </div>
    </div>

    <!-- Delivery -->
    <div class="mb-4">
      <label class="block text-sm font-medium">Delivery</label>
      <div class="grid grid-cols-2 gap-2">
        <button
          v-for="region in deliveryRegions"
          :key="region"
          :class="{ 'border-blue-500': filters.delivery === region }"
          class="border p-2 rounded-md text-center"
          @click="filters.delivery = region"
        >
          {{ region }}
        </button>
      </div>
    </div>

    <!-- Buttons -->
    <div class="flex gap-2 mt-4">
      <button class="bg-blue-600 text-white p-2 rounded-md w-full">Apply filters</button>
      <button class="border p-2 rounded-md w-full" @click="clearFilters">Clear all</button>
    </div>
  </div>
</template>
<script setup lang="ts">
  import { initFlowbite } from 'flowbite'

  onMounted(() => {
    initFlowbite()
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
