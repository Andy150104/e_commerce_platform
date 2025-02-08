<template>
  <div class="bg-gray-100 min-h-screen">
    <div class="container mx-auto py-8">
      <h1 class="text-3xl font-bold text-center mb-8">Image Gallery</h1>
      <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
        <!-- Image Thumbnails -->
        <div v-for="(image, index) in images" :key="index" class="relative">
          <img :src="image" alt="Gallery Image" class="w-full h-48 object-cover rounded-lg cursor-pointer" @click="openLightbox(index)" />
        </div>
      </div>
    </div>

    <!-- Lightbox -->
    <div v-if="isLightboxOpen" class="fixed inset-0 bg-black bg-opacity-80 flex justify-center items-center z-50">
      <!-- Previous Button -->
      <button
        class="absolute left-4 text-white bg-gray-800 hover:bg-gray-700 rounded-full w-12 h-12 flex items-center justify-center shadow-lg focus:outline-none"
        @click.stop="previousImage"
      >
        &larr;
      </button>

      <!-- Main Image -->
      <img v-if="currentImageIndex !== null" :src="images[currentImageIndex]" alt="Expanded Image" class="max-w-full max-h-full" />

      <!-- Next Button -->
      <button
        class="absolute right-4 text-white bg-gray-800 hover:bg-gray-700 rounded-full p-2 w-12 h-12 flex items-center justify-center shadow-lg focus:outline-none"
        @click.stop="nextImage"
      >
        &rarr;
      </button>

      <!-- Close Overlay -->
      <button
        class="absolute top-4 right-4 text-white bg-gray-800 hover:bg-gray-700 rounded-full p-2 w-12 h-12 focus:outline-none"
        @click="closeLightbox"
      >
        ✕
      </button>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref } from 'vue'

  // Image array (replace with actual URLs)
  const images = ref([
    'https://images.unsplash.com/photo-1554629947-334ff61d85dc?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1024&h=1280&q=80',
    'https://images.pexels.com/photos/3618162/pexels-photo-3618162.jpeg',
    'https://cdn.devdojo.com/images/june2023/mountains-10.jpeg',
    'https://images.unsplash.com/photo-1520350094754-f0fdcac35c1c?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80',
  ])

  // State
  const isLightboxOpen = ref(false)
  const currentImageIndex = ref<number | null>(null)

  // Open Lightbox
  const openLightbox = (index: number) => {
    currentImageIndex.value = index
    isLightboxOpen.value = true
  }

  // Close Lightbox
  const closeLightbox = () => {
    isLightboxOpen.value = false
    currentImageIndex.value = null
  }

  // Navigate to the next image
  const nextImage = () => {
    if (currentImageIndex.value !== null) {
      currentImageIndex.value = (currentImageIndex.value + 1) % images.value.length
    }
  }

  // Navigate to the previous image
  const previousImage = () => {
    if (currentImageIndex.value !== null) {
      currentImageIndex.value = (currentImageIndex.value - 1 + images.value.length) % images.value.length
    }
  }
</script>

<style scoped>
  /* Styling for buttons */
  button {
    transition: background-color 0.2s;
  }
</style>
