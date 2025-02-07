<template>
  <BaseControlTextField
    :xml-column="xmlColumns.mailAddress"
    :maxlength="50"
    :disabled="false"
    :err-msg="fieldErrors.mailAddress"
    :placeholder="'00000@tsumitate.com'"
  />
  <BaseControlTextField
    :xml-column="xmlColumns.mailAddressConfirm"
    :maxlength="50"
    :disabled="false"
    :err-msg="fieldErrors.mailAddressConfirm"
    :placeholder="'00000@tsumitate.com'"
  />
  <BaseControlTextField
    :xml-column="xmlColumns.passWord"
    :maxlength="50"
    :disabled="false"
    :type="'password'"
    :err-msg="fieldErrors.passWord"
    :placeholder="'******'"
  />
  <Field id="password" name="password" type="password" />
  <ModelFullScreen :is-open-modal="true">
    <template #body>
      <ImageCrop :images="myImages" />
    </template>
  </ModelFullScreen>
  <!-- Video Container -->
  <div class="video-container">
    <iframe
      src="https://www.youtube.com/embed/EHyfI3c6L3A?autoplay=1&loop=1&playlist=EHyfI3c6L3A&start=10&end=20&mute=1"
      frameborder="0"
      allow="autoplay; encrypted-media; picture-in-picture"
      allowfullscreen
      class="video-iframe"
    ></iframe>
  </div>

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
import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue';
import ModelFullScreen from '@PKG_SRC/components/Modal/ModelFullScreen.vue';
import { useMyppStore } from '@PKG_SRC/stores/Modules/Mypp/Mypp';
import { XmlLoadColumn } from '@PKG_SRC/utils/xml';
import { storeToRefs } from 'pinia';
import { Field, useForm } from 'vee-validate';

// Import Flowbite JavaScript
import 'flowbite';
import '@popperjs/core';
import { initPopovers } from 'flowbite';
import ImageCrop from '@PKG_SRC/components/CropImage/ImageCrop.vue';

const store = useMyppStore();
const { fieldValues, fieldErrors } = storeToRefs(store);
const formContext = useForm({ initialValues: fieldValues.value });
store.SetFields(formContext);
onMounted(() =>{
  initPopovers()
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
    rules: 'required',
    visible: true,
    option: '',
  }),
  passWord: XmlLoadColumn({
    id: 'passWord',
    name: 'Password',
    rules: '',
    visible: true,
    option: ''
  }),
};
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
