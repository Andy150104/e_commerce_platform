<template>
  <div class="p-4">
    <!-- Button to open the Galleria in fullscreen -->
    <Button label="Show" icon="pi pi-external-link" @click="onClickOpenImage" severity="contrast" />

    <!-- Galleria -->
    <Galleria
      v-model:visible="visible"
      v-model:activeIndex="activeIndex"
      :value="processedImages"
      :responsiveOptions="responsiveOptions"
      :showThumbnails="true"
      :circular="true"
      :numVisible="3"
      :fullScreen="true"
      containerStyle="max-width: 640px"
    >
      <!-- Large image template -->
      <template #item="slotProps">
        <img :src="slotProps.item.itemImageSrc" :alt="slotProps.item.alt" style="width: 640px; display: block; height: 400px; object-fit: cover" />
      </template>

      <!-- Thumbnail template -->
      <template #thumbnail="slotProps">
        <img
          :src="slotProps.item.thumbnailImageSrc"
          :alt="slotProps.item.alt"
          style="width: 100%; display: block; height: 80px; width: 160px; object-fit: cover;"
        />
      </template>
    </Galleria>
  </div>
</template>

<script setup lang="ts">
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
  import { computed, ref } from 'vue'
  import type { PropType } from 'vue'

  interface ImageItem {
    itemImageSrc: string
    thumbnailImageSrc: string
    alt: string
  }

  const props = defineProps({
    images: {
      type: Array as PropType<any[]>,
      default: () => [],
    },
  })

  const processedImages = computed<ImageItem[]>(() =>
    props.images.map((img: any) => ({
      itemImageSrc: img.itemImageSrc || img.imageUrl,
      thumbnailImageSrc: img.thumbnailImageSrc || img.imageUrl,
      alt: img.alt || 'Image',
    }))
  )

  const onClickOpenImage = () => {
    if (processedImages.value.length > 0) {
      visible.value = true
      return
    }
    const formMessageStore = useFormMessageStore()
    formMessageStore.SetFormMessageNotApiRes('E00000', true, 'Can not open because there are not any images!!!')
  }

  const visible = ref(false)
  const activeIndex = ref(0)
  const responsiveOptions = ref([
    { breakpoint: '1300px', numVisible: 4 },
    { breakpoint: '575px', numVisible: 1 },
  ])
</script>

<style scoped>
  /* Your optional styling */
</style>
