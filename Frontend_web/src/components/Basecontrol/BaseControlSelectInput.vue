<template>
  <!-- flex justify-center sẽ giúp canh giữa phần tử bên trong -->
  <div class="flex justify-center">
    <Field
      :id="fieldId"
      as="select"
      :name="xmlColumn.id"
      :rules="fieldRules"
      @blur="onBlur"
      @change="onChange"
      class="w-full text-center items-center hover:bg-gray-100 font-medium bg-white border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 px-4 py-2"
    >
      <option v-for="option in options" :key="option.value" :value="option.value">
        {{ option.label }}
      </option>
    </Field>
  </div>
</template>

<script setup lang="ts">
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { Field } from 'vee-validate'
  import { uuid } from 'vue-uuid'

  const props = defineProps({
    model: {
      type: [String, Number],
      required: false,
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    disabled: {
      type: Boolean,
      required: false,
    },
    params: {
      type: Object,
      required: false,
      default: () => ({}),
    },
    masterName: {
      type: String as () => MasterName,
      required: true,
    },
    isDisable: {
      type: Boolean,
      required: false,
      default: false,
    },
    isShowError: {
      type: Boolean,
      required: false,
      default: true,
    },
  })
  const options = ref<any>([])

  const fieldId = uuid.v4()
  const fieldRules = ref(props.xmlColumn.rules)

  interface Emits {
    (e: 'on-blur'): void
    (e: 'on-change'): void
  }

  const emit = defineEmits<Emits>()

  const onBlur = () => {
    emit('on-blur')
  }

  const onChange = () => {
    emit('on-change')
  }

  const fetch = async (params = {}) => {
    await nextTick()
    options.value = await getSelectComponentData(props.masterName, {
      xmlColumn: props.xmlColumn,
      ...(props.params as any),
    })
  }
  const onFocus = () => {
    const id = document.getElementById(fieldId) as HTMLInputElement
    id.focus()
  }
  onMounted(async () => {
    await nextTick()
    await fetch()
  })
  defineExpose({
    fetch,
    onFocus,
  })
</script>
