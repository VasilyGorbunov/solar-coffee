<template>
   <solar-modal>
      <template v-slot:header>
         Receive Shipment
      </template>
      <template v-slot:body>
         <label for="product">Product Received:</label>
         <select v-model="selectedProduct" class="shipmentItems" id="product">
            <option disabled value="">Please select one</option>
            <option v-for="item in inventory" :value="item" :key="item.product.id">
               {{ item.product.name }}
            </option>
         </select>
         <label for="qtyReceived">Quantity Received:</label>
         <input type="number" id="qtyReceived" v-model="qtyReceived">
      </template>
      <template v-slot:footer>
         <solar-button
           type="button"
           aria-label="save new shipment"
           @click.native="save"
         >
            Save Received Shipment
         </solar-button>
         <solar-button
           type="button"
           aria-label="close modal"
           @click.native="close"
         >
            Close
         </solar-button>
      </template>
   </solar-modal>
</template>

<script lang="ts">
   import {Component, Prop, Vue} from 'vue-property-decorator'
    import SolarButton from "../SolarButton.vue";
    import SolarModal from "@/components/modals/SolarModal.vue";
   import {IProduct, IProductInventory} from "@/types/Product";
   import {IShipment} from "@/types/Shipment";

    @Component({
        name: "ShipmentModal",
        components: {SolarModal, SolarButton}
    })
    export default class ShipmentModal extends Vue {
       @Prop({required: true, type: Array as () => IProductInventory[] })
       inventory!: IProductInventory[];

       selectedProduct: IProduct = {
          createdOn: new Date(),
          updatedOn: new Date(),
          id: 0,
          name: '',
          description: '',
          price: 0,
          isTaxable: false,
          isArchived: false
       };

       qtyReceived: number = 0;

       save() {
         const shipment: IShipment = {
            productId: this.selectedProduct.id,
            adjustment: this.qtyReceived
         };

         this.$emit('save:shipment', shipment);
       }

       close() {
          this.$emit('close');
       }

    }
</script>

<style scoped>

</style>