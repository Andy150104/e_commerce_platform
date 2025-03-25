<template>
    <BaseScreenDashBoard :data-theme="getTheme()">
        <template #body>
            <h1 class="page-title">My Failed Exchange list!</h1>
            
        <div v-if="failedExchangeList.length > 0" class="exchange-list">
          <div v-for="item in failedExchangeList" :key="item.exchangeId" class="exchange-card">
            <div class="exchange-image-container" @click="openGallery(item.exchangeId ?? '')">
              <img class="exchange-image" :src="getFirstImage(item)" :alt="item.exchangeName" />
            </div>
            <div class="exchange-content">
              <div class="exchange-text">
                <h3 class="exchange-name">{{ item.exchangeName || 'N/A' }}</h3>
                <p class="exchange-description" v-html="item.description || 'N/A'"></p>
                <p class="exchange-status" :class="getStatusClass(item.status)">
                  {{ getStatusText(item.status) }}
                </p>
              </div>
              <div class="mt-6 sm:gap-4 sm:items-center sm:flex sm:mt-8">
                <ModalInputForm :header="'Send recheck exchange request form!'" :button-icon="'pi pi-plus'" :button-label="'Send Now!'"
                  :button-severity="'contrast'" :width="'80rem'"  @on-confirm="() => { console.log('⚡ onConfirm Triggered!', item.exchangeId); onConfirm(item.exchangeId || ''); }" @on-blinding="onBinding">
                  <template #body>
                    <BaseControlEditorInput :xml-column="xmlColumns.description" v-model="editorValue" />
                  </template>
                </ModalInputForm>
              </div>
            </div>
          </div>
        </div>
        <div v-else class="empty-message"> 
          <div class="exchange-card">
            <p class="page-title"> Không có dữ liệu trao đổi (Pending/Changing)</p>
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
import BaseControlEditorInput from '@PKG_SRC/components/Basecontrol/BaseControlEditorInput.vue';
import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue';
import type { AEPSGetByIdExchangeAccessoryEntity } from '@PKG_SRC/composables/Client/api/@types';
import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue';
import { useExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeStore';
import { useFailedEXStore } from '@PKG_SRC/stores/Modules/DashBoard/PlannedCustomer/RecheckRequestEX';
import { XmlLoadColumn } from '@PKG_SRC/utils/xml';
import Button from 'primevue/button';
import { useForm } from 'vee-validate';
import { ref } from 'vue';



const myFailedExchangeStore = useFailedEXStore()
const ExchangeStore = useExchangeStore()
const failedExchangeList = computed(() => myFailedExchangeStore.failedExchangeList || [])
const displayFullScreen = ref(false);
const activeIndex = ref(0);
const currentImages = ref<{ itemImageSrc: string; alt: string }[]>([]);
const data = ref<AEPSGetByIdExchangeAccessoryEntity[]>([]); 
const displayGallery = ref(false);  
const editorValue = ref('')

const { fieldValues, fieldErrors } = storeToRefs(myFailedExchangeStore)
const formContext = useForm({ initialValues: fieldValues.value })
myFailedExchangeStore.SetFields(formContext)
const xmlColumns = {
  description: XmlLoadColumn({
    id: 'description',
    name: 'Description',
    rules: '',
    visible: true,
    option: '',
  }),
  exchangeId: XmlLoadColumn({
    id: 'exchangeId',
    name: 'ExchangeId',
    rules: '',
    visible: true,
    option: '',
  }),
}
const onBinding = async (items: any) => {
  await nextTick()
  myFailedExchangeStore.fields.setFieldValue('description', items.description)
  editorValue.value = items.description
}
const onConfirm = async (exchangeid: string) => {
  if (!myFailedExchangeStore.fields) {
    console.error("Store chưa được khởi tạo đúng.");
    return;
  }

  if (!exchangeid) {
    console.warn("Không có ExchangeId để submit.");
    return;
  }

  myFailedExchangeStore.fields.setFieldValue("exchangeId", exchangeid);
  myFailedExchangeStore.fields.setFieldValue("description", editorValue.value);

  console.log("Submitting với ExchangeId:", exchangeid);
  console.log("Description:", editorValue.value);

  await nextTick();
  await myFailedExchangeStore.SendRecheckRequest();
};

const openGallery = (exchangeId: string) => {
  console.log('Clicked on exchangeId:', exchangeId); 

  const selectedItem = failedExchangeList.value.find((item:any) => item.exchangeId === exchangeId);
  console.log('Selected item:', selectedItem);

  // Kiểm tra xem `details` có tồn tại hay không
  const imageList = (selectedItem as any)?.details?.imageBlindBoxList || selectedItem?.imageBlindBoxList || [];


  if (imageList.length === 0) {
    console.warn('No images found for this exchange.');
    return;
  }

  console.log('Image list:', imageList);

  currentImages.value = imageList.map((img : any) => ({
    itemImageSrc: img.imageUrls?.trim() !== '' ? img.imageUrls 
      : 'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=',
    alt: 'Gallery image'
  }));

  console.log('Gallery opened with images:', currentImages.value);
  displayGallery.value = true;
};


watch(currentImages, (newVal) => {
    if (newVal.length > 0) {
      displayGallery.value = true // Chỉ mở modal khi đã có ảnh
    }
  })
  const openFullScreen = (index: number) => {
  console.log('Opening Galleria at index:', index);
  if (currentImages.value.length === 0) {
    console.warn('No images to open in Galleria.');
    return;
  }
  activeIndex.value = index; // Đặt index đúng ảnh được chọn
  displayFullScreen.value = true;
};
const onCloseFullScreen = () => {
  displayFullScreen.value = false;
  displayGallery.value = true; // Mở lại modal danh sách ảnh nhỏ
};
const getFirstImage = (item: any) => {
  return item.details?.imageBlindBoxList?.[0]?.imageUrls || 'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=';
};
const getTheme = () => {
  return localStorage.getItem('nuxt-color-mode') === '0' ? 'dark' : 'light';
};
const getStatusClass = (status: number | null | undefined) => {
  switch (status) {
    case 4: return 'status-rejected';
    default: return '';
  }
};

const getStatusText = (status: number | null | undefined) => {
  switch (status) {
    case 4: return 'Rejected';
    default: return 'Unknown';
  }
};
onMounted(async () => {
  await myFailedExchangeStore.GetFailedExchangeList()
  console.log("Danh sách failedExchangeList sau khi fetch:", myFailedExchangeStore.failedExchangeList);

  data.value = myFailedExchangeStore.failedExchangeList || [];
});
</script>
<style>
.exchange-list {
  display: grid;
  grid-template-columns: 1fr;
  gap: 1.5rem;
  padding: 1rem;
  max-width: 1200px;
  margin: 0 auto;
}


.exchange-card {
  display: flex;
  flex-direction: column;
  background-color: var(--card-light);
  color: var(--text-light);
  border: 1px solid #ddd;
  padding: 1.5rem;
  border-radius: 12px;
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  gap: 1rem;
}
@media (min-width: 768px) {
  .exchange-card {
    flex-direction: row;
    align-items: center;
  }
}

  .exchange-card:hover {
    transform: scale(1.02);
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
.status-rejected {
  background-color: #ffdddd;
  color: #d9534f;
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
  .gallery-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
    gap: 10px;
  }
  .gallery-thumbnail {
    cursor: pointer;
    border-radius: 5px;
    overflow: hidden;
    display: flex;
    justify-content: center;
    align-items: center;
}

.gallery-thumbnail img {
  width: 100%;
  height: auto;
  object-fit: contain; /* Hoặc object-fit: cover nếu muốn cắt ảnh */
  display: block;
}


.gallery-thumbnail:hover img {
  transform: scale(1.05);
}
  .exchange-name {
    white-space: normal;
    overflow-wrap: break-word;
    word-break: break-word;
  font-size: 1.2rem;
  font-weight: bold;
  margin: 0;
}

.exchange-description {
  font-size: 0.9rem;
  color: #555;
  word-break: break-word;
  white-space: normal;
  overflow-wrap: break-word;
}
.exchange-image-container {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.exchange-image {
  max-width: 15rem;
  height: auto;
  object-fit: contain;  border-radius: 8px; /* Bo góc nhẹ */
}


  .exchange-content {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.exchange-text {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
}


.mt-6 {
  width: 100%;
  display: flex;
  justify-content: center;
  margin-top: 1rem;
}

@media (min-width: 768px) {
  .exchange-content {
    flex-direction: row;
    align-items: center;
  }
  .mt-6 {
    width: auto;
    justify-content: flex-end;
    margin-top: 0;
  }
}

</style>