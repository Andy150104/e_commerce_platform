<template>
  <div class="flex flex-col min-h-screen dark:bg-gray-900">
    <!-- Header -->
    <header class="sticky top-0 z-40 w-full">
      <NavBar />
    </header>
    <!-- Body -->
    <div class="max-w-8xl mr-4 ml-4 px-4 sm:px-6 md:px-8 sm:mx-14 md:mx-auto 3xl:mx-96 min-h-[calc(100vh-64px)]">
      <slot name="body"></slot>
    </div>
    <FooterControl />
  </div>
</template>
<script lang="ts" setup>
  import FooterControl from '@PKG_SRC/components/Footer/FooterControl.vue'
  import NavBar from '@PKG_SRC/components/NavBar/NavBar.vue'
</script>
