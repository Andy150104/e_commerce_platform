import{q as d,Y as n,y as c,x as l,C as u,z as g}from"./DFVm4NPh.js";const o={quantity:"1"},f=o,h={values:o,errors:f,...g},m=d("Detail",{state:()=>({fields:h,productDetail:{}}),getters:{fieldValues:e=>e.fields.values,fieldErrors:e=>e.fields.errors},actions:{SetFields(e){this.fields=e},ResetStore(){this.fields.resetForm()},convertImageList(e){const s="https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=";(!Array.isArray(e)||e.length===0)&&(e=[]);let t=e.map((r,i)=>({src:r.imageUrl,alt:`Ảnh ${i+1}`}));for(;t.length<5;)t.push({src:s,alt:`Ảnh bổ sung ${t.length+1}`});return t},async AddProductToCart(e){const a=n(),s=c();s.LoadingChange(!0);const t=l(),r=u(this.fields.values,o),i=await a.api.v1.DPSInsertCart.$post({body:{codeAccessory:e,quantity:Number(r.quantity)}});return s.LoadingChange(!1),i.success?(t.SetFormMessage(i,!0),!0):!1},async AddOneProductToCart(e){const a=n(),s=c();s.LoadingChange(!0);const t=l();u(this.fields.values,o);const r=await a.api.v1.DPSInsertCart.$post({body:{codeAccessory:e,quantity:1}});return s.LoadingChange(!1),r.success?(t.SetFormMessage(r,!0),!0):!1},async GetProductById(e){const a=n(),s=c();s.LoadingChange(!0);const t=await a.api.v1.DPSSelectAccessory.$get({query:{CodeAccessory:e}});return s.LoadingChange(!1),this.productDetail=t.response??{},!!t.success}}});export{m as u};
