<template>
  <LabelItem :xml-column="xmlColumn" />
  <BaseControlRadioButton
    ref="radioButtonRef"
    :model="model"
    :xml-column="xmlColumn"
    :disabled="disabled"
    :master-name="masterName"
    :params="params"
  />
</template>
<script setup lang="ts">
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import BaseControlRadioButton from '../Basecontrol/BaseControlRadioButton.vue'
  import LabelItem from '../Basecontrol/LabelItem.vue'

  defineProps({
    model: {
      type: [String, Number],
      required: true,
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
    disabled: {
      type: Boolean,
      required: true,
    },
    move: {
      type: Boolean,
      required: false,
      default: true,
    },
    masterName: {
      type: String as () => MasterName,
      required: true,
    },
    params: {
      type: Object,
      required: false,
      default: () => ({}),
    },
    labelClass: {
      type: String,
      required: false,
      default: '',
    },
    formClass: {
      type: String,
      required: false,
      default: '',
    },
  })
</script>
