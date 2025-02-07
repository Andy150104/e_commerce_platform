import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'

export const fieldsInitialize = {
  dantaiBangou: '',
  ryokouBangou: '',
  ninsyoKey: '',
  mailAddress: '',
  mailAddressConfirm: '',
  seinengappi: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = fieldsInitialize

// VeeValidate
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  // vee-validate用の初期化メソッド
  ...veeValidateStateInitialize,
}

// Định nghĩa type ProductMypp111
export type ProductMypp111 = {
  codeProduct: string // Mã sản phẩm
  nameProduct: string // Tên sản phẩm
  rating: number // Đánh giá sản phẩm
  price: string // Giá sản phẩm
  salePrice: string // Giá khuyến mãi
  moneyUnit: string // Đơn vị tiền tệ
  shortDescription: string // Mô tả ngắn
  isWishlist: number // Có trong danh sách yêu thích hay không (0 hoặc 1)
  imageUrl: string[] // Danh sách URL hình ảnh sản phẩm
}
export type ImageList = {
  response: ImageListResponse
}

export type ImageListResponse = {
  imagesList: string[]
}

const sampleImageList: ImageList = {
  response: {
    imagesList: [
      'https://toysfuntasy.com/cdn/shop/files/Labubu-V1-3_5efb670d-e921-42ee-aa56-cd11bc57460b.jpg?v=1688770128', 
      'https://au.popmart.com/cdn/shop/files/2_b9ca6ef4-f32b-4956-8912-838c77889fd5_1024x1024.jpg?v=1705996646'
    ],
  },
}
// Dữ liệu mẫu (sample data)
const sampleData: ProductMypp111[] = [
  {
    codeProduct: 'M0001',
    nameProduct: 'PlayStation 5 Slim Console',
    rating: 3.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0002',
    nameProduct: 'Iphone 8',
    rating: 5.0,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0003',
    nameProduct: 'Molecule Man',
    rating: 4.0,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
  {
    codeProduct: 'M0004',
    nameProduct: 'Iphone 16 Pro Max',
    rating: 4.5,
    price: '450000',
    salePrice: '450000',
    moneyUnit: 'VNĐ',
    shortDescription: 'Up to 120fps with 120Hz output, 1TB HDD, 2 Controllers, Ray Tracing',
    isWishlist: 0,
    imageUrl: [
      'https://www.apple.com/newsroom/images/2024/09/apple-debuts-iphone-16-pro-and-iphone-16-pro-max/tile/Apple-iPhone-16-Pro-hero-geo-240909-lp.jpg.landing-big_2x.jpg',
      'https://www.zdnet.com/a/img/resize/9f3fcf92f17d47c88823e7f2c0f1454ecd3e5140/2024/09/19/8da68e24-08b1-467a-9062-a90a96c1d879/dsc02198.jpg?auto=webp&fit=crop&height=900&width=1200',
      'https://cdn.mos.cms.futurecdn.net/AVEcca7TuDmt8wjaFZPkzj.jpg',
    ],
  },
]

export type Mypg020MailState = {
  fields: typeof fields
  produtList: ProductMypp111[]
  imageList: ImageListResponse
}

export const useDisplayProductStore = defineStore('Product', {
  state: (): Mypg020MailState => ({
    fields,
    produtList: [],
    imageList: {} as ImageListResponse,
  }),
  // state

  getters: {
    fieldValues: (state) => {
      return state.fields.values
    },
    fieldErrors: (state) => {
      return state.fields.errors
    },
  },
  actions: {
    SetFields(value: any) {
      this.fields = value
    },
    ResetStore() {
      this.fields.resetForm()
    },
    GetProductList() {
      this.produtList = sampleData
    },
    GetImageList() {
      this.imageList = sampleImageList.response
    },
  },
})
