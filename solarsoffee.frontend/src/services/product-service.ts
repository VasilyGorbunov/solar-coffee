import axios from 'axios';

export class ProductService {
  API_URL = process.env.VUE_APP_API_URL;

  async archive(productId: number) {
    let result: any = await axios.patch(`${this.API_URL}/product/${productId}`);
    return result.data;
  }
}