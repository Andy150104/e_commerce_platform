<template>
  <div class="container mx-auto px-6 py-8">
    <!-- Header -->
    <div class="text-center mb-8">
      <h1 class="text-4xl font-bold text-gray-900">Shopping Cart</h1>
    </div>

    <div class="grid lg:grid-cols-3 gap-8">
      <!-- Cart Items -->
      <div class="lg:col-span-2 space-y-6">
        <div v-for="(item, index) in cartItems" :key="index" class="flex items-center bg-white shadow rounded-lg overflow-hidden border">
          <!-- Product Image -->
          <img :src="item.image" alt="Product Image" class="w-32 h-32 object-cover" />

          <!-- Product Details -->
          <div class="flex-1 p-4">
            <h2 class="text-lg font-bold text-gray-900">{{ item.name }}</h2>
            <p class="text-sm text-gray-500">{{ item.description }}</p>
            <p class="text-lg font-semibold text-gray-900 mt-2">${{ item.price }}</p>
          </div>

          <!-- Quantity Control -->
          <div class="flex items-center space-x-3 px-4">
            <button class="px-3 py-2 bg-gray-300 hover:bg-gray-400 text-gray-900 rounded-full text-lg" @click="decreaseQuantity(index)">−</button>
            <span class="text-lg font-semibold">{{ item.quantity }}</span>
            <button class="px-3 py-2 bg-gray-300 hover:bg-gray-400 text-gray-900 rounded-full text-lg" @click="increaseQuantity(index)">+</button>
          </div>

          <!-- Remove Button -->
          <button class="px-3 py-2 text-red-600 hover:text-red-800 font-semibold" @click="removeItem(index)">Remove</button>
        </div>
      </div>

      <!-- Order Summary -->
      <div class="lg:col-span-1 space-y-6">
        <div class="p-6 bg-white shadow rounded-lg border">
          <h2 class="text-xl font-bold text-gray-900">Order Summary</h2>
          <div class="mt-4 space-y-2">
            <div class="flex justify-between text-gray-600">
              <span>Original Price:</span>
              <span>${{ originalPrice }}</span>
            </div>
            <div class="flex justify-between text-gray-600">
              <span>Savings:</span>
              <span>-${{ discount }}</span>
            </div>
            <div class="flex justify-between text-gray-600">
              <span>Store Pickup:</span>
              <span>$99</span>
            </div>
            <div class="flex justify-between text-gray-600">
              <span>Tax:</span>
              <span>$799</span>
            </div>
            <div class="flex justify-between text-lg font-bold text-gray-900 mt-4">
              <span>Total:</span>
              <span>${{ totalPrice }}</span>
            </div>
          </div>
          <button class="mt-6 w-full py-3 bg-blue-600 hover:bg-blue-700 text-white rounded-lg text-lg font-semibold">Proceed to Checkout</button>
          <a href="#" class="block text-center text-blue-600 hover:underline mt-4"> Continue Shopping → </a>
        </div>

        <!-- Voucher -->
        <div class="p-6 bg-white shadow rounded-lg border">
          <h2 class="text-lg font-bold text-gray-900">Do you have a voucher or gift card?</h2>
          <div class="mt-4 flex">
            <input type="text" placeholder="Enter code" class="flex-1 px-4 py-2 border rounded-l-lg focus:outline-none focus:ring" />
            <button class="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-r-lg">Apply Code</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref, computed } from 'vue'

  const cartItems = ref([
    {
      name: 'PC system All in One APPLE iMac',
      description: "24' Retina 4.5K, 8GB, SSD 256GB",
      price: 1499,
      quantity: 2,
      image:
        'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
    },
    {
      name: 'Apple Watch Series 8',
      description: '41mm Aluminum Case with Midnight Sport Band',
      price: 598,
      quantity: 1,
      image:
        'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
    },
    {
      name: "MacBook Pro 16'",
      description: 'M3 Pro chip, 36GB Memory, 512GB SSD',
      price: 1799,
      quantity: 1,
      image:
        'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
    },
  ])

  const decreaseQuantity = (index: number) => {
    if (cartItems.value[index].quantity > 1) {
      cartItems.value[index].quantity--
    }
  }

  const increaseQuantity = (index: number) => {
    cartItems.value[index].quantity++
  }

  const removeItem = (index: number) => {
    cartItems.value.splice(index, 1)
  }

  const originalPrice = computed(() => cartItems.value.reduce((total, item) => total + item.price * item.quantity, 0))

  const discount = computed(() => Math.floor(originalPrice.value * 0.05)) // Giảm giá 5%
  const totalPrice = computed(() => originalPrice.value - discount.value + 99 + 799)
</script>
