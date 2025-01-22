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
  <div class="video-container">
    <iframe
      src="https://www.youtube.com/embed/EHyfI3c6L3A?autoplay=1&loop=1&playlist=EHyfI3c6L3A&start=10&end=20&mute=1"
      frameborder="0"
      allow="autoplay; encrypted-media; picture-in-picture"
      allowfullscreen
      class="video-iframe"
    ></iframe>
  </div>
</template>

<script setup lang="ts">
import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue';
import { useMyppStore } from '@PKG_SRC/stores/Modules/Mypp/Mypp';
import { XmlLoadColumn } from '@PKG_SRC/utils/xml';
import { storeToRefs } from 'pinia';
import { useForm } from 'vee-validate';

const store = useMyppStore();
const { fieldValues, fieldErrors } = storeToRefs(store);
const formContext = useForm({ initialValues: fieldValues.value });
store.SetFields(formContext);

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
};
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
