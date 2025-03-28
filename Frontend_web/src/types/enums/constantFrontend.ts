export enum Theme {
  DARK = '0',
  LIGHT = '1',
  SYSTEM = '3',
}
export enum UserInfoGrpId {
  personal = '111',
  group = '121',
}
export enum DateFormat {
  YYYYMMDD = 'YYYY-MM-DD',
  MMDDYYYY = 'MM-DD-YYYY',
  DDMMYYYY = 'DD-MM-YYYY',
}
export enum Currency {
  USD = 'USD',
  VND = 'VND',
  EUR = 'EUR',
  JPY = 'JPY',
  GBP = 'GBP',
  AUD = 'AUD',
  CAD = 'CAD',
}

export enum Locale {
  EN_US = 'en-US',
  VI_VN = 'vi-VN',
  DE_DE = 'de-DE',
  JA_JP = 'ja-JP',
  FR_FR = 'fr-FR',
  ES_ES = 'es-ES',
}

export enum StepStatus {
  currentStep = 0,
  pendingStep = 1,
  finishStep = 2,
}
export interface selectItem {
  text: string
  value: string
}

export enum Address {
  Province = '0',
  District = '1',
  Ward = '2',
  Other = '3',
}
export enum Catogry {
  ParentCategory = '0',
  ChildCategory = '1',
}

export enum StatusOrder {
  Processing = 1,
  Success = 2,
  Failed = 3,
}
