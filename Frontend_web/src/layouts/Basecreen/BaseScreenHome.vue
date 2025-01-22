<template>
  <div class="flex flex-col min-h-screen dark:bg-gray-900">
    <!-- Header -->
    <header class="sticky top-0 z-40 w-full">
      <NavBar />
    </header>
    <!-- Body -->
    <div class="max-w-8xl mt-4 p-8 shadow-lg mx-4 min-h-[calc(100vh-64px)]">
      <slot name="body"></slot>
    </div>
    <FooterControl />
  </div>
</template>
<script lang="ts" setup>
  import FooterControl from '@PKG_SRC/components/Footer/FooterControl.vue'
  import NavBar from '@PKG_SRC/components/NavBar/NavBar.vue'
</script>
