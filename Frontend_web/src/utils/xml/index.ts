interface xmlFormat {
  id: string
  name: string
  rules: string
  visible: string
  DefaultOption: string
}
export interface xmlColumn {
  id: string
  name: string
  rules: string
  visible: boolean
  option: string
}
let fields: { [name: string]: xmlFormat } = {}

export const XmlLoadColumn = (column: xmlColumn) => {
  if (!CheckFieldId(column.id)) {
    return column
  }
  // nameがあれば上書き
  const name = fields[column.id]?.name
  if (name != null && name !== '') {
    column.name = name
  }
  // rulesがあれば上書き
  const rules = fields[column.id]?.rules
  if (rules != null && rules !== '') {
    column.rules = rules
  }
  // visibleがあれば上書き
  const visible = fields[column.id]?.visible
  if (visible != null && visible !== '') {
    column.visible = visible.toString().toLowerCase() === 'true'
  }

  // optionがあれば上書き
  const option = fields[column.id]?.DefaultOption
  if (option != null && option !== '') {
    column.option = option
  }
  return column
}

export const CheckFieldId = (fieldId: string) => {
  return fields[fieldId]
}
function hasIdProperty(obj: any): obj is { id: unknown } {
  return typeof obj === 'object' && obj !== null && 'id' in obj
}
