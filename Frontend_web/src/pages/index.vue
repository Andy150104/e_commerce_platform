<template>
  <nav class="bg-white border-gray-200 dark:bg-gray-900">
    <div class="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
      <a href="https://flowbite.com/" class="flex items-center space-x-3 rtl:space-x-reverse">
        <img src="https://flowbite.com/docs/images/logo.svg" class="h-8" alt="Flowbite Logo" />
        <span class="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">Flowbite</span>
      </a>
      <div class="flex items-center md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse">
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

  <LocationPicker
    :xml-column-province="xmlColumns.mailAddressConfirm"
    :maxlength-province="50"
    :disabled-province="false"
    :err-msg-province="fieldErrors.mailAddressConfirm"
    :placeholder-province="'Province'"
    :xml-column-district="xmlColumns.mailAddress"
    :maxlength-district="50"
    :disabled-district="false"
    :err-msg-district="fieldErrors.mailAddress"
    :placeholder-district="'District'"
    :xml-column-ward="xmlColumns.passWord"
    :maxlength-ward="50"
    :disabled-ward="false"
    :err-msg-ward="fieldErrors.passWord"
    :placeholder-ward="'Ward'"
  />
  <Field id="password" name="password" type="password" />
  <ModelFullScreen :is-open-modal="true">
    <template #body>
      <ImageCrop :images="myImages" />
    </template>
  </ModelFullScreen>
  <!-- Video Container
  <div class="video-container">
    <iframe
    src="https://www.youtube.com/embed/EHyfI3c6L3A?autoplay=1&loop=1&playlist=EHyfI3c6L3A&start=10&end=20&mute=1"
    frameborder="0"
    allow="autoplay; encrypted-media; picture-in-picture"
    allowfullscreen
    class="video-iframe"
    ></iframe>
  </div> -->
  <GalleryCarousel :images="imageList" />
  <UserControlUploadImage :max-number-image="1" :is-show-popover="false" :label="'Upload avatar image'" />
  <TableComponent />
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

  // Import Flowbite JavaScript
  import 'flowbite'
  import '@popperjs/core'
  import { initDropdowns, initPopovers } from 'flowbite'
  import ImageCrop from '@PKG_SRC/components/CropImage/ImageCrop.vue'
  import DropDownSearch from '@PKG_SRC/components/DropDown/DropDownSearch.vue'
  import { useAddressStore } from '@PKG_SRC/utils/address/store/addressStore'
  import LocationPicker from '@PKG_SRC/components/UserControl/LocationPicker.vue'
  import GalleryCarousel from '@PKG_SRC/components/Gallery/GalleryCarousel.vue'
  import TableComponent from '@PKG_SRC/components/Table/TableComponent.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  const imageStore = useUploadImageStore()
  imageStore.SetUploadImage('a', 1024, 'https://res.cloudinary.com/dbfokyruf/image/upload/v1739683116/fehfhddpzfgcpfrkkfoj.jpg')
  const imageList = [
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-1.jpg', alt: 'Ảnh 1' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-2.jpg', alt: 'Ảnh 2' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-3.jpg', alt: 'Ảnh 3' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-4.jpg', alt: 'Ảnh 4' },
    { src: 'https://flowbite.s3.amazonaws.com/docs/gallery/square/image-9.jpg', alt: 'Ảnh 5' },
  ]
  const store = useMyppStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
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
  }
  const myImages = ref([
    { original: 'https://assets.teenvogue.com/photos/624c93e41741df0bc53718a2/4:3/w_3839,h_2879,c_limit/blackpink%20rose%20phone%20case.jpg' },
    { original: 'https://phongcachlamdep.com/wp-content/uploads/2022/04/kieu-toc-cua-rose-black-pink.jpg' },
  ])
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
