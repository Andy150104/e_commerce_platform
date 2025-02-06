import type { StepItem } from '@PKG_SRC/types';
import { defineStore } from 'pinia';

export type StepState = {
    steppList: StepItem[]
}

export const useStepperStore = defineStore('stepper', {
  state: (): StepState => ({
    steppList: [],
  }),
  actions: {
    SetValues(value: any) {
        this.steppList = value
    },
    updateStepStatus(index: number, newStatus: number) {
      if (index >= 0 && index < this.steppList.length) {
        this.steppList[index].status = newStatus;
      } else {
        console.warn('Invalid step index');
      }
    },
    resetStepper() {
      this.steppList.forEach((step) => {
        step.status = 0;
      });
    },
  },
});
