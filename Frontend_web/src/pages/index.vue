<template>
  <nav class="bg-white border-gray-200 dark:bg-gray-900">
    <div class="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
      <a href="https://flowbite.com/" class="flex items-center space-x-3 rtl:space-x-reverse">
        <img src="https://flowbite.com/docs/images/logo.svg" class="h-8" alt="Flowbite Logo" />
        <span class="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">Flowbite</span>
      </a>
      <div class="flex items-center md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse">
        <UserControlTextFieldLabel
        :xml-column="xmlColumns.test"
                :maxlength="50"
                :disabled="false"
                :type="'password'"
                :err-msg="fieldErrors.test"
                :placeholder="'***********'"
        />
        <button
          type="button"
          class="flex text-sm bg-gray-800 rounded-full md:me-0 focus:ring-4 focus:ring-gray-300 dark:focus:ring-gray-600"
          id="user-menu-button"
          aria-expanded="false"
          data-dropdown-toggle="user-dropdown"
          data-dropdown-placement="bottom"
        >
          <span class="sr-only">Open user menu</span>
          <img class="w-8 h-8 rounded-full" src="" alt="user photo" />
        </button>
        <!-- Dropdown menu -->
        <div
          class="z-50 hidden my-4 text-base list-none bg-white divide-y divide-gray-100 rounded-lg shadow-sm dark:bg-gray-700 dark:divide-gray-600"
          id="user-dropdown"
        >
          <div class="px-4 py-3">
            <span class="block text-sm text-gray-900 dark:text-white">Bonnie Green</span>
            <span class="block text-sm text-gray-500 truncate dark:text-gray-400">name@flowbite.com</span>
          </div>
          <ul class="py-2" aria-labelledby="user-menu-button">
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Dashboard</a
              >
            </li>
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Settings</a
              >
            </li>
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Earnings</a
              >
            </li>
            <li>
              <a
                href="#"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                >Sign out</a
              >
            </li>
          </ul>
        </div>
        <button
          data-collapse-toggle="navbar-user"
          type="button"
          class="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
          aria-controls="navbar-user"
          aria-expanded="false"
        >
          <span class="sr-only">Open main menu</span>
          <svg class="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 14">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 1h15M1 7h15M1 13h15" />
          </svg>
        </button>
      </div>
      <div class="items-center justify-between hidden w-full md:flex md:w-auto md:order-1" id="navbar-user">
        <ul
          class="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:space-x-8 rtl:space-x-reverse md:flex-row md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700"
        >
          <li>
            <a
              href="#"
              class="block py-2 px-3 text-white bg-blue-700 rounded-sm md:bg-transparent md:text-blue-700 md:p-0 md:dark:text-blue-500"
              aria-current="page"
              >Home</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >About</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >Services</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >Pricing</a
            >
          </li>
          <li>
            <a
              href="#"
              class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >Contact</a
            >
          </li>
        </ul>
      </div>
    </div>
  </nav>
  <div>
      <UserControlSelectCategory
        :xml-column-parent-category="xmlColumns.mailAddress"
        :maxlength-parent-category="50"
        :disabled-parent-category="false"
        :err-msg-parent-category="fieldErrors.mailAddress"
        :placeholder-parent-category="'Parent Category'"
        :xml-column-child-category="xmlColumns.mailAddressConfirm"
        :maxlength-child-category="50"
        :disabled-child-category="false"
        :err-msg-child-category="fieldErrors.mailAddressConfirm"
        :placeholder-child-category="'Child Category'"
      />
    <BaseControlPriceRange :items="products" :columns="columns" />
    <ModalInputForm header="Edit Profile" width="25rem" buttonLabel="Edit Profile">
      <template #header>
        <div class="inline-flex items-center gap-2">
          <Avatar image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png" shape="circle" />
          <span class="font-bold whitespace-nowrap">Amy Elsner</span>
        </div>
      </template>

      <LocationPicker
        :xml-column-province="xmlColumns.mailAddress"
        :maxlength-province="50"
        :disabled-province="false"
        :err-msg-province="fieldErrors.mailAddress"
        :placeholder-province="'Province'"
        :xml-column-district="xmlColumns.mailAddressConfirm"
        :maxlength-district="50"
        :disabled-district="false"
        :err-msg-district="fieldErrors.mailAddressConfirm"
        :placeholder-district="'District'"
        :xml-column-ward="xmlColumns.passWord"
        :maxlength-ward="50"
        :disabled-ward="false"
        :err-msg-ward="fieldErrors.passWord"
        :placeholder-ward="'Ward'"
      />

      <template #footer>
        <Button label="Cancel" text severity="secondary" />
        <Button label="Save" outlined severity="primary" />
      </template>
    </ModalInputForm>
  </div>
  <GalleryCarousel :images="imageList" />
  <ModalPopupScreen
    @ok-click="
      () => {
        const loadingStore = useLoadingStore()
        loadingStore.LoadingChange(true)
        for (let i = 1; i <= 100000; i++) {
          console.log(i)
        }
        loadingStore.LoadingChange(false)
      }
    "
    cancel-button-name="No, Cancel It"
    label-name="Delete"
    ok-button-name="Yes, let delete!!"
  />
  <button
    class="text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"
  >
    Gradient Button
  </button>
  <UserControlUploadImage :max-number-image="1" :is-show-popover="false" :label="'Upload avatar image'" />
  <TableComponent />
  <TableNuxtUIComponent />
  <!-- Button to trigger Popover -->
  <div
    data-popover-target="popover-default"
    class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
  >
    Default popover
  </div>
  <!-- Popover Content -->
  <div
    data-popover
    id="popover-default"
    role="tooltip"
    class="absolute z-10 invisible inline-block w-64 text-sm text-gray-500 transition-opacity duration-300 bg-white border border-gray-200 rounded-lg shadow-xs opacity-0 dark:text-gray-400 dark:border-gray-600 dark:bg-gray-800"
  >
    <div class="px-3 py-2 bg-gray-100 border-b border-gray-200 rounded-t-lg dark:border-gray-600 dark:bg-gray-700">
      <h3 class="font-semibold text-gray-900 dark:text-white">Popover title</h3>
    </div>
    <div class="px-3 py-2">
      <p>And here's some amazing content. It's very engaging. Right?</p>
    </div>
    <div data-popper-arrow></div>
  </div>
</template>

<script setup lang="ts">
  import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue'
  import ModelFullScreen from '@PKG_SRC/components/Modal/ModelFullScreen.vue'
  import { useMyppStore } from '@PKG_SRC/stores/Modules/Mypp/Mypp'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { storeToRefs } from 'pinia'
  import { Field, useForm } from 'vee-validate'
  import 'swiper/swiper-bundle.css'
  // Import Flowbite JavaScript
  import 'flowbite'
  import '@popperjs/core'
  import { initDropdowns, initPopovers } from 'flowbite'
  import ImageCrop from '@PKG_SRC/components/CropImage/ImageCrop.vue'
  import DropDownSearch from '@PKG_SRC/components/DropDown/DropDownSearch.vue'
  import { useAddressStore } from '@PKG_SRC/stores/Third Modules/address/addressStore'
  import LocationPicker from '@PKG_SRC/components/UserControl/LocationPicker.vue'
  import GalleryCarousel from '@PKG_SRC/components/Gallery/GalleryCarousel.vue'
  import TableComponent from '@PKG_SRC/components/Table/TableComponent.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import ModalPartScreen from '@PKG_SRC/components/Modal/ModalPartScreen.vue'
  import { useProfileStore } from '@PKG_SRC/stores/Modules/DashBoard/profileStore'
  import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
  import ModalPopupScreen from '@PKG_SRC/components/Modal/ModalPopupScreen.vue'
  import { useLoadingStore } from '@PKG_SRC/stores/Modules/usercontrol/loadingStore'
  import BaseControlQuantityInput from '@PKG_SRC/components/Basecontrol/BaseControlQuantityInput.vue'
  import 'swiper/swiper-bundle.css'
  import { Swiper, SwiperSlide, useSwiper } from 'swiper/vue'
  import BaseControlPriceRange from '@PKG_SRC/components/Basecontrol/BaseControlPriceRange.vue'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
import UserControlSelectCategory from '@PKG_SRC/components/UserControl/UserControlSelectCategory.vue'
  const imageStore = useUploadImageStore()
  imageStore.SetUploadImage('a', 1024, 'https://res.cloudinary.com/dbfokyruf/image/upload/v1739683116/fehfhddpzfgcpfrkkfoj.jpg')
  const imageList = [
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-1.jpg', alt: 'Ảnh 1' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-2.jpg', alt: 'Ảnh 2' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-3.jpg', alt: 'Ảnh 3' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-4.jpg', alt: 'Ảnh 4' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-9.jpg', alt: 'Ảnh 5' },
  ]
  const handleOkClick = () => {
    return false
  }
  const store = useMyppStore()
  const storeProfile = useProfileStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const isOpen = ref(false)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  onMounted(async () => {
    initPopovers()
    initDropdowns()
    const addressStore = useAddressStore()
  })
  const xmlColumns = {
    mailAddress: XmlLoadColumn({
      id: 'mailAddress',
      name: 'メールアドレス',
      rules: 'required',
      visible: true,
      option: '',
    }),
    mailAddressConfirm: XmlLoadColumn({
      id: 'mailAddressConfirm',
      name: 'メールアドレス（確認）',
      rules: '',
      visible: true,
      option: '',
    }),
    passWord: XmlLoadColumn({
      id: 'passWord',
      name: 'Password',
      rules: '',
      visible: true,
      option: '',
    }),
    test: XmlLoadColumn({
      id: 'test',
      name: 'Password',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const myImages = ref([
    { original: 'https://assets.teenvogue.com/photos/624c93e41741df0bc53718a2/4:3/w_3839,h_2879,c_limit/blackpink%20rose%20phone%20case.jpg' },
    { original: 'https://phongcachlamdep.com/wp-content/uploads/2022/04/kieu-toc-cua-rose-black-pink.jpg' },
  ])

  const products = [
    {
      id: '1000',
      code: 'f230fh0g3',
      name: 'Bamboo Watch',
      description: 'Product Description',
      image: 'bamboo-watch.jpg',
      price: 65,
      category: 'Accessories',
      quantity: 24,
      inventoryStatus: 'INSTOCK',
      rating: 5,
    },
    {
      id: '1001',
      code: 'nvklal433',
      name: 'Black Watch',
      description: 'Product Description',
      image: 'black-watch.jpg',
      price: 72,
      category: 'Accessories',
      quantity: 61,
      inventoryStatus: 'INSTOCK',
      rating: 4,
    },
    {
      id: '1002',
      code: 'zz21cz3c1',
      name: 'Blue Band',
      description: 'Product Description',
      image: 'blue-band.jpg',
      price: 79,
      category: 'Fitness',
      quantity: 2,
      inventoryStatus: 'LOWSTOCK',
      rating: 3,
    },
    {
      id: '1003',
      code: '244wgerg2',
      name: 'Blue T-Shirt',
      description: 'Product Description',
      image: 'blue-t-shirt.jpg',
      price: 29,
      category: 'Clothing',
      quantity: 25,
      inventoryStatus: 'INSTOCK',
      rating: 5,
    },
    {
      id: '1004',
      code: 'h456wer53',
      name: 'Bracelet',
      description: 'Product Description',
      image: 'bracelet.jpg',
      price: 15,
      category: 'Accessories',
      quantity: 73,
      inventoryStatus: 'INSTOCK',
      rating: 4,
    },
    {
      id: '1005',
      code: 'av2231fwg',
      name: 'Brown Purse',
      description: 'Product Description',
      image: 'brown-purse.jpg',
      price: 120,
      category: 'Accessories',
      quantity: 0,
      inventoryStatus: 'OUTOFSTOCK',
      rating: 4,
    },
    {
      id: '1006',
      code: 'bib36pfvm',
      name: 'Chakra Bracelet',
      description: 'Product Description',
      image: 'chakra-bracelet.jpg',
      price: 32,
      category: 'Accessories',
      quantity: 5,
      inventoryStatus: 'LOWSTOCK',
      rating: 3,
    },
    {
      id: '1007',
      code: 'mbvjkgip5',
      name: 'Galaxy Earrings',
      description: 'Product Description',
      image: 'galaxy-earrings.jpg',
      price: 34,
      category: 'Accessories',
      quantity: 23,
      inventoryStatus: 'INSTOCK',
      rating: 5,
    },
    {
      id: '1008',
      code: 'vbb124btr',
      name: 'Game Controller',
      description: 'Product Description',
      image: 'game-controller.jpg',
      price: 99,
      category: 'Electronics',
      quantity: 2,
      inventoryStatus: 'LOWSTOCK',
      rating: 4,
    },
    {
      id: '1009',
      code: 'cm230f032',
      name: 'Gaming Set',
      description: 'Product Description',
      image: 'gaming-set.jpg',
      price: 299,
      category: 'Electronics',
      quantity: 63,
      inventoryStatus: 'INSTOCK',
      rating: 3,
    },
    {
      id: '1010',
      code: 'plb34234v',
      name: 'Gold Phone Case',
      description: 'Product Description',
      image: 'gold-phone-case.jpg',
      price: 24,
      category: 'Accessories',
      quantity: 0,
      inventoryStatus: 'OUTOFSTOCK',
      rating: 4,
    },
    {
      id: '1011',
      code: '4920nnc2d',
      name: 'Green Earbuds',
      description: 'Product Description',
      image: 'green-earbuds.jpg',
      price: 89,
      category: 'Electronics',
      quantity: 23,
      inventoryStatus: 'INSTOCK',
      rating: 4,
    },
    {
      id: '1012',
      code: '250vm23cc',
      name: 'Green T-Shirt',
      description: 'Product Description',
      image: 'green-t-shirt.jpg',
      price: 49,
      category: 'Clothing',
      quantity: 74,
      inventoryStatus: 'INSTOCK',
      rating: 5,
    },
    {
      id: '1013',
      code: 'fldsmn31b',
      name: 'Grey T-Shirt',
      description: 'Product Description',
      image: 'grey-t-shirt.jpg',
      price: 48,
      category: 'Clothing',
      quantity: 0,
      inventoryStatus: 'OUTOFSTOCK',
      rating: 3,
    },
    // ... các sản phẩm khác
  ]

  // Cấu hình các cột hiển thị
  const columns = [
    {
      field: 'name',
      header: 'Name',
      isFilter: true,
      isSort: true,
      filterType: 'text', // hoặc 'inputText'
    },
    {
      field: 'description',
      header: 'Description',
      isFilter: true,
      filterType: 'text',
    },
    {
      field: 'price',
      header: 'Price',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'category',
      header: 'Category',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'inventoryStatus',
      header: 'Status',
      isFilter: true,
      isSort: false,
      filterType: 'multiSelect', // Dùng MultiSelect
      options: ['INSTOCK', 'LOWSTOCK', 'OUTOFSTOCK'],
    },
    {
      field: 'rating',
      header: 'Rating',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
  ]
</script>

<style scoped>
  .video-container {
    position: relative;
    width: 100%;
    max-width: 800px;
    margin: 20px auto;
    aspect-ratio: 16 / 9;
  }

  .video-iframe {
    width: 100%;
    height: 100%;
    border: none;
  }
</style>
