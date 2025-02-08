import { defineStore } from 'pinia'

/** 認証 */
type Mypm720GrantKbn = {
  /** 大カテゴリID */
  mainCategoryId?: string | undefined
  /** 大カテゴリ名 */
  mainCategoryNm?: string | undefined
  /** サブカテゴリリスト */
  subCategoryList?: Mypm720GrantKbnSubCategory[] | undefined
  /** フォームリスト(大カテゴリ) */
  mainFormList?: Mypm720GrantKbnForm[] | undefined
}
export type Mypm720GrantKbnSubCategory = {
  /** 中カテゴリID */
  subCategoryId?: string | undefined
  /** 大カテゴリ名 */
  subCategoryNm?: string | undefined
  /** フォームリスト */
  formList?: Mypm720GrantKbnForm[] | undefined
}
export type Mypm720GrantKbnForm = {
  /** フォーム名:フォーム名　Ex)WFDfsm030 */
  formNm?: string | undefined
  /** ﾌﾟﾛｸﾞﾗﾑ名称1:全角 */
  prgNm1?: string | undefined
  /** ﾌﾟﾛｸﾞﾗﾑ名称2:全角 */
  prgNm2?: string | undefined
  /** 権限区分:0:使用不可,1:参照,3:印刷,5:更新,7:リカバリ,9:フルコントロール【コード表：権限区分】 */
  grantKbn?: string | undefined
}

export const useUserStore = defineStore('user', {
  state: () => ({
    UserInfo: {
      usrId: '',
      usrNm: '',
      dfltGrpId: '',
      grpId: '111',
      grpNm: '',
      bondedAreaKbn: 0,
      needPasswordUpdate: false,
    },
    Mypm730: [
      {
        mainCategoryId: '',
        mainCategoryNm: '',
        subCategoryList: [
          {
            subCategoryId: '',
            subCategoryNm: '',
            formList: [{ formNm: '', prgNm1: '', prgNm2: '', grantKbn: '' }],
          },
        ],
        mainFormList: [{ formNm: '', prgNm1: '', prgNm2: '', grantKbn: '' }],
      },
    ] as Mypm720GrantKbn[],
    isGroupAuth: false, //2024/09/11追加
    executeTopPage: false, // TOPページによる更新かどうか(localStorageの更新無限ループ対策)
  }),
  // stateと同じgetterは宣言不要
  getters: {},
  actions: {
    // 初期化
    ResetStore() {
      this.UserInfo = {
        usrId: '',
        usrNm: '',
        dfltGrpId: '',
        grpId: '',
        grpNm: '',
        bondedAreaKbn: 0,
        needPasswordUpdate: false,
      }
      this.Mypm730 = [
        {
          mainCategoryId: '',
          mainCategoryNm: '',
          subCategoryList: [
            {
              subCategoryId: '',
              subCategoryNm: '',
              formList: [{ formNm: '', prgNm1: '', prgNm2: '', grantKbn: '' }],
            },
          ],
          mainFormList: [{ formNm: '', prgNm1: '', prgNm2: '', grantKbn: '' }],
        },
      ]
      this.isGroupAuth = false //2024/09/11追加
    },
    async SetMypm730(grpId: string, isTopPage: boolean) {
      // apiで取得（引数として権限など）
      //   const mypm720SelectMyselfRequest: Mypm720SelectMyselfRequest = {
      //     apiCallerId: '',
      //     isOnlyValidation: false,
      //     grpId: grpId,
      //   }
      //   const apiClient = useApiClient()
      //   const res = apiClient.api.v1.Mypm720SelectMyself.$post({
      //     body: mypm720SelectMyselfRequest,
      //   })
      //   await res.then((result: Mypm720SelectMyselfResponse) => {
      //     // TOPページから呼ばれた場合はメッセージを表示するためにメッセージIDを変更する
      //     if (isTopPage && result.messageId === 'I99999')
      //       result.messageId = 'I00001'
      //     // const formMessageStore = useFormMessageStore()
      //     // formMessageStore.SetFormMessage(result as AbstractApiResponseOfString)
      //     if (result.success === false) return
      //     // 成功時
      //     this.UserInfo.usrId = result.response?.usrId ?? ''
      //     this.UserInfo.usrNm = result.response?.usrNm ?? ''
      //     this.UserInfo.dfltGrpId = result.response?.dfltGrpId ?? ''
      //     this.UserInfo.grpId = result.response?.grpId ?? ''
      //     this.UserInfo.grpNm = result.response?.grpNm ?? ''
      //     this.UserInfo.needPasswordUpdate =
      //       result.response?.needPasswordUpdate ?? true
      //     if (result.response?.mypm720GrantKbnList)
      //       this.Mypm730 = result.response?.mypm720GrantKbnList
      //     this.executeTopPage = isTopPage
      //   })
      return true
    },
    SetGroupAuth(isGroupAuth: boolean) {
      this.isGroupAuth = isGroupAuth //2024/09/11追加
    },
  },
  persist: true, // 永続化
})
