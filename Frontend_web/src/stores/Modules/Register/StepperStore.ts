import type { StepItem } from '@PKG_SRC/types'
import { StepStatus } from '@PKG_SRC/types/enums/constantFrontend'
import { defineStore } from 'pinia'

export type StepState = {
  steppList: StepItem[]
  currentIndex: number
}

export const useStepperStore = defineStore('stepper', {
  state: (): StepState => ({
    steppList: [],
    currentIndex: 0,
  }),
  actions: {
    SetValues(value: any) {
      this.steppList = value
    },
    updateStepStatus(nameItem: string, newStatus: number) {
      const step = this.steppList.find((item) => item.nameItem === nameItem)
      if (step) {
        step.status = newStatus
      }
    },
    resetStepper() {
      this.steppList.forEach((step) => {
        step.status = 0
      })
    },
    moveToNextStep() {
      this.currentIndex += 1
      this.steppList[this.currentIndex].status = StepStatus.currentStep
    },
    moveToPreviousStep() {
      if (this.currentIndex === 0) return
      this.steppList[this.currentIndex].status = StepStatus.pendingStep
      this.currentIndex -= 1
    },
  },
})
