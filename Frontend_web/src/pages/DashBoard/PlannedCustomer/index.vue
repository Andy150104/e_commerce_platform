<template>
  <BaseScreenDashBoard :data-theme="getTheme()">
    <template #body>
      <h1 class="page-title">Manage Exchange Requests</h1>

      <!-- Filter Panel -->
      <div class="filter-panel">
        <input v-model="searchQuery" type="text" placeholder="🔍 Exchange name search..." class="filter-input" />
        <select v-model="selectedStatus" class="filter-select">
          <option value="">All</option>
          <option value="1">⏳ Pending</option>
          <option value="2">⏳ Changing</option>
          <option value="3">✅ Approved</option>
          <option value="4">❌ Rejected</option>
        </select>
      </div>

      <!-- Tab Menu -->
      <TabMenu :model="tabs" v-model:activeIndex="selectedTabIndex" class="custom-tabmenu" />

      <!-- Nội dung của từng tab -->
      <div v-if="selectedTabIndex === 0">
        <div v-if="pendingAndChangingList.length > 0" class="exchange-list">
          <div v-for="item in pendingAndChangingList" :key="item.exchangeId" class="exchange-card">
            <div class="exchange-image-container" @click="openGallery(item.exchangeId ?? '')">
              <img class="exchange-image" :src="getFirstImage(item)" :alt="item.exchangeName" />
            </div>
            <div class="exchange-content">
              <div class="exchange-text">
                <h3 class="exchange-name">{{ item.exchangeName || 'N/A' }}</h3>
                <p class="exchange-description">{{ item.description || 'N/A' }}</p>
                <p class="exchange-status" :class="getStatusClass(item.status)">
                  {{ getStatusText(item.status) }}
                </p>
              </div>
              <button class="btn-manage" @click="manageRequest(item.exchangeId ?? '')">Manage Request</button>
            </div>
          </div>
        </div>
        <div v-else class="empty-message"> 
          <div class="exchange-card">
            <p class="page-title">📭 Không có dữ liệu trao đổi (Pending/Changing)</p>
          </div>
        </div>
        
      </div>

      <div v-if="selectedTabIndex === 1">
        <div v-if="CompletedStatusList.length > 0" class="exchange-list">
          <div v-for="item in CompletedStatusList" :key="item.exchangeId" class="exchange-card">
            <div class="exchange-image-container" @click="openGallery(item.exchangeId ?? '')">
              <img class="exchange-image" :src="getFirstImage(item)" :alt="item.exchangeName" />
            </div>
            <div class="exchange-content">
              <div class="exchange-text">
                <h3 class="exchange-name">{{ item.exchangeName || 'N/A' }}</h3>
                <p class="exchange-description">{{ item.description || 'N/A' }}</p>
                <p class="exchange-status" :class="getStatusClass(item.status)">
                  {{ getStatusText(item.status) }}
                </p>
              </div>
              <button class="btn-manage" @click="manageRequest(item.exchangeId ?? '')">Manage Request</button>
            </div>
          </div>
        </div>
        <div v-else class="empty-message"> 
          <div class="exchange-card">
            <p class="page-title">📭 Không có dữ liệu trao đổi (Succeed/Failed)</p>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
   <!-- Popup Modal chứa danh sách ảnh nhỏ -->
  <Dialog v-model:visible="displayGallery" modal header="Image Gallery" :style="{ width: '80vw' }"  >
    <div class="gallery-grid">
      <div v-for="(image, index) in currentImages" :key="index" class="gallery-thumbnail" @click="openFullScreen(index)">
        <img :src="image.itemImageSrc" :alt="image.alt" />
      </div>
    </div>
  </Dialog>

  <!-- Galleria để xem ảnh lớn -->
  <Galleria
    v-model:activeIndex="activeIndex"
    v-model:visible="displayFullScreen"
    :value="currentImages"
    containerStyle="max-width: 850px"
    :fullScreen="true"
    :showThumbnails="false"
    :showItemNavigators="true"
    :showThumbnailNavigators="true"
    @update:visible="onCloseFullScreen"
    
  >
    <template #item="slotProps" @mousedown="closeGalleryOnOutsideClick">
      <img :src="slotProps.item.itemImageSrc" class="gallery-image" />
    </template>
  </Galleria>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
import { useMyExchangeStore } from '../../../stores/Modules/DashBoard/PlannedCustomer/MyExchangeStore'
import TabMenu from 'primevue/tabmenu';
import type { AEPSGetExchangeAccessoryEntity, AEPSGetExchangeAccessoryImageBlindBoxList } from '@PKG_SRC/composables/Client/api/@types'

const authStore = useAuthStore()
const myExchangeStore = useMyExchangeStore()
const router = useRouter()
const searchQuery = ref('')
const selectedStatus = ref('')
const exchangeList = computed(() => myExchangeStore.uAEPSGetExchangeAccessoryEntities || [])
const displayGallery = ref(false);
const displayFullScreen = ref(false);
const activeIndex = ref(0);
const currentImages = ref<{ itemImageSrc: string; alt: string }[]>([]);
const data = ref<AEPSGetExchangeAccessoryEntity[]>([]); 
  const selectedTabIndex = ref(0);
  const tabs = ref([
  { label: "Pending & Changing exchanges", icon: "pi pi-clock" },
  { label: "Completed exchanges", icon: "pi pi-check" },
]);

const filteredExchangeList = computed(() => {
  return exchangeList.value.filter((item) => {
    const isPendingOrChanging = item.status === 1 || item.status === 2; 
    const matchSearch = searchQuery.value
      ? item.exchangeName?.toLowerCase().includes(searchQuery.value.toLowerCase())
      : true;
    return isPendingOrChanging && matchSearch;
  });
});
const pendingAndChangingList = computed(() => {
  return exchangeList.value.filter((item) => item.status === 1 || item.status === 2);
});

const CompletedStatusList = computed(() => {
  return exchangeList.value.filter((item) => item.status !== 1 && item.status !== 2);
});


const openGallery = (exchangeId: string) => {
  console.log('Clicked on exchangeId:', exchangeId); // Kiểm tra xem có chạy không

  const selectedItem = exchangeList.value.find((item) => item.exchangeId === exchangeId);
  if (!selectedItem || !selectedItem.imageBlindBoxList || selectedItem.imageBlindBoxList.length === 0) {
    console.warn('No images found for this exchange.');
    return;
  }

  currentImages.value = selectedItem.imageBlindBoxList.map((img) => ({
    itemImageSrc: img.imageUrls || '/placeholder.png',
    alt: 'Gallery image'
  }));

  displayGallery.value = true;
  console.log('Gallery opened:', currentImages.value); // Kiểm tra dữ liệu ảnh
};
watch(currentImages, (newVal) => {
    if (newVal.length > 0) {
      displayGallery.value = true // Chỉ mở modal khi đã có ảnh
    }
  })


const openFullScreen = (index: number) => {
  console.log('Opening Galleria at index:', index);
  activeIndex.value = index; // Đặt index đúng ảnh được chọn
  displayFullScreen.value = true; // Mở Galleria
};
const onCloseFullScreen = () => {
    displayFullScreen.value = false // Đóng Galleria
    displayGallery.value = true // Mở lại modal danh sách ảnh nhỏ
  }
const getFirstImage = (item: any) => {
  return item.imageBlindBoxList?.[0]?.imageUrls || '/placeholder.png';
};

const getStatusClass = (status: number | null | undefined) => {
  switch (status) {
    case 1: return 'status-pending';
    case 2: return 'status-changing';
    case 3: return 'status-approved';
    case 4: return 'status-rejected';
    default: return '';
  }
};

const getStatusText = (status: number | null | undefined) => {
  switch (status) {
    case 1: return '⏳ Pending';
    case 2: return '🔄 Changing';
    case 3: return '✅ Success';
    case 4: return '❌ Rejected';
    default: return 'Unknown';
  }
};

const manageRequest = (selectedProduct: string) => {
  if (authStore.isAuthorization) {
    router.push(`/DashBoard/PlannedCustomer/Exchange-${selectedProduct}`)
  }
};

onMounted(async () => {
  await myExchangeStore.GetMyExchange()
  data.value = myExchangeStore.uAEPSGetExchangeAccessoryEntities || [];
});

const getTheme = () => {
  return localStorage.getItem('nuxt-color-mode') === '0' ? 'dark' : 'light';
};
</script>

<style scoped>
  :root {
    --bg-light: #ffffff;
    --bg-dark: #121212;
    --text-light: #000000;
    --text-dark: #ffffff;
    --card-light: #f8f9fa;
    --card-dark: #1e1e1e;
  }
  [data-theme='light'] {
    background-color: var(--bg-light);
    color: var(--text-light);
  }

  [data-theme='dark'] {
    background-color: var(--bg-dark);
    color: var(--text-dark);
  }
  .exchange-list {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
  }

  .exchange-card {
  display: flex;
  align-items: stretch; /* Đảm bảo các phần tử con có cùng chiều cao */
  justify-content: space-between;
  background-color: var(--card-light);
  color: var(--text-light);
  border: 1px solid #ddd;
  padding: 2rem;
  border-radius: 12px;
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  gap: 2rem;
  min-height: 180px; /* Không để card bị quá nhỏ */
}
[data-theme="dark"] .exchange-card {
  background-color: var(--card-dark);
  color: var(--text-dark);
  border: 1px solid #444;
}

  .exchange-card:hover {
    transform: scale(1.02);
  }
  .exchange-status {
  font-size: 0.9rem;
  font-weight: bold;
  padding: 0.3rem 0.6rem;
  border-radius: 5px;
  display: inline-block;
  width: fit-content;
  margin-top: 0.5rem; /* Đẩy status xuống dưới */
}

  .status-pending {
    color: #ff9800;
    background-color: #fff3e0;
  }
  .status-changing {
  color: #fdc153; /* Màu cam để thể hiện thay đổi */
  background-color: #fff3e0;
}

  .status-approved {
  background-color: #dff0d8;
  color: #5cb85c;
}

  .status-rejected {
  background-color: #ffdddd;
  color: #d9534f;
}
  .exchange-image-container {
    flex-shrink: 0;
    width: 150px;
    height: 150px;
    overflow: hidden;
    border-radius: 12px;
    cursor: pointer;
  }

  .exchange-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .exchange-content {
    flex-grow: 1;
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
  }
  .exchange-text {
  flex: 1; /* Để phần chữ mở rộng mà không đẩy nút */
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
}

.exchange-name {
  font-size: 1.2rem;
  font-weight: bold;
  margin: 0;
}

.exchange-description {
  font-size: 0.9rem;
  color: #555;
  word-wrap: break-word;
  overflow-wrap: break-word;
}
/* Fix cho Manage Request luôn căn giữa */
.btn-manage {
  align-self: center;
  flex-shrink: 0; /* Ngăn không cho nút bị bóp lại */
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  cursor: pointer;
  transition: 0.2s;
}

  .btn-manage:hover {
    background-color: #0056b3;
  }

  /* Modal danh sách ảnh nhỏ */
  .gallery-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
    gap: 10px;
  }

  .gallery-thumbnail {
    cursor: pointer;
    border-radius: 5px;
    overflow: hidden;
  }

  .gallery-thumbnail img {
    width: 100%;
    height: auto;
    border-radius: 5px;
    transition: transform 0.2s ease-in-out;
  }

  .gallery-thumbnail:hover img {
    transform: scale(1.05);
  }

  /* Ảnh lớn trong gallery */
  .gallery-image {
    width: 100%;
    height: auto;
    display: block;
    max-height: 80vh;
  }
  .p-galleria-nav-button {
    pointer-events: auto !important;
    z-index: 10000 !important;
    display: flex !important;
    opacity: 1 !important;
  }

  .p-galleria-prev-button,
  .p-galleria-next-button {
    width: 50px !important;
    height: 50px !important;
    background: rgba(0, 0, 0, 0.5) !important;
    border-radius: 50%;
  }
  .exchange-name {
    color: black !important;
  }
  [data-theme='dark'] .exchange-name {
    color: white !important;
  }
/* 🎨 PANEL CHỨA FILTER */
.filter-panel {
  display: flex;
  gap: 1rem;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  background: white;
  max-width: 800px;
  width: 100%;
  margin: 0 auto;
  margin-bottom: 1.5rem;
}
[data-theme='dark'] .filter-panel {
  background: black;
}

/* Input & Select */
.filter-input,
.filter-select {
  flex: 1;
  padding: 0.6rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 1rem;
  color: black;
  background: white;
}
.filter-select {
  max-width: 200px;
}
[data-theme='dark'] .filter-input,
[data-theme='dark'] .filter-select {
  color: white;
  background: black;
  border: 1px solid #555;
}
.page-title {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 1.5rem;
  text-align: center;
  color: black;
}
[data-theme='dark'] .page-title {
  color: white;
}
.custom-tabmenu .p-tabmenu-nav {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.custom-tabmenu .p-tabmenu-nav li {
  flex: 1;
  text-align: center;
}

</style>
