<template>
  <div>
    <div v-if="isInline" class="md:flex md:space-x-4 space-y-4 md:space-y-0">
      <LabelItem :xml-column="xmlColumn" />
      <DateTimePicker :xml-column="xmlColumn" :err-msg="errMsg" :disabled="false" :maxlength="maxlength" :date-model="dateModel" />
    </div>
    <div v-else>
      <LabelItem :xml-column="xmlColumn" />
      <DateTimePicker
        :xml-column="xmlColumn"
        :err-msg="errMsg"
        :disabled="false"
        :maxlength="maxlength"
        :date-model="dateModel"
        :date-picker-position="datePickerPosition"
      />
    </div>
  </div>
</template>
<script setup lang="ts">
  import { type PropType } from 'vue'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { _padding } from '#tailwind-config/theme'
  import { className } from '@PKG_SRC/utils/class/className'
  import LabelItem from '../Basecontrol/LabelItem.vue'
  import DateTimePicker from '../Basecontrol/DateTimePicker.vue'
  const props = defineProps({
    dateModel: {
      type: String,
      required: true,
      default: '',
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsg: {
      type: String,
      required: true,
      default: '',
    },
    move: {
      type: Boolean,
      required: false,
      default: true,
    },
    disabled: {
      type: Boolean,
      required: true,
    },
    maxlength: {
      type: Number,
      required: true,
    },
    placeholder: {
      type: String,
      required: false,
      default: '',
    },
    classField: {
      type: String,
      default: className.INPUT_LABEL_FLOAT,
    },
    isShowError: {
      type: Boolean,
      required: false,
      default: true,
    },
    readonly: {
      type: Boolean,
      required: false,
      default: false,
    },
    isInline: {
      type: Boolean,
      required: false,
      default: true,
    },
    datePickerPosition: {
      type: String,
      required: false,
      default: 'bottom left',
    },
  })
</script>
<style scoped></style>
