<template>
    <div class="inventory-container">
        <h1 id="inventoryTitle">
            Inventory DashBoard
        </h1>
        <hr/>
        <div class="inventory-actions">
            <solar-button @click.native="showNewProductModal" id="addNewBtn">
                Add New Item
            </solar-button>
            <solar-button @click.native="showNewShipmentModal" id="receiveShipmentBtn">
                Recieve Shipment
            </solar-button>
        </div>
        <table class="table" id="inventoryTable">
            <tr>
                <th>Item</th>
                <th>Quantity On-hand</th>
                <th>Unit Price</th>
                <th>Taxable</th>
                <th>Delete</th>
            </tr>
            <tr v-for="item in inventory" :key="item.id">
                <td>{{ item.product.name }}</td>
                <td :class="`${applyColor(item.quantityOnHand, item.idealQuantity)}`">
                    {{ item.quantityOnHand }}
                </td>
                <td>{{ item.product.price | price}}</td>
                <td>
                    <span v-if="item.product.isTaxable">Yes</span>
                    <span v-else>No</span>
                </td>
                <td>
                    <div
                      class="lni lni-cross-circle product-archive"
                      @click="archiveProduct(item.product.id)"
                    ></div>
                </td>
            </tr>
        </table>
        <new-product-modal
          v-if="isNewProductVisible"
          @save:product="saveNewProduct"
          @close="closeModal"
        />
        <shipment-modal
          v-if="isShipmentVisible"
          :inventory="inventory"
          @save:shipment="saveNewShipment"
          @close="closeModal"
        />
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator'
    import {IProduct, IProductInventory} from "@/types/Product";
    import SolarButton from "@/components/SolarButton.vue";
    import ShipmentModal from "@/components/modals/ShipmentModal.vue";
    import NewProductModal from "@/components/modals/NewProductModal.vue";
    import {IShipment} from "@/types/Shipment";
    import {InventoryService} from "@/services/inventory-service";
    import {ProductService} from "@/services/product-service";

    const inventoryService = new InventoryService();
    const productService = new ProductService();

    @Component({
        name: "Inventory",
        components: {NewProductModal, ShipmentModal, SolarButton}
    })
    export default class Inventory extends Vue {
        isNewProductVisible: boolean = false;
        isShipmentVisible: boolean = false;

        inventory: IProductInventory[] = [];

        async archiveProduct(productId: number) {
            await productService.archive(productId);
            await this.initialize();
        }

        async saveNewProduct(newProduct: IProduct) {
            await productService.save(newProduct);
            this.isNewProductVisible = false;
            await this.initialize();
        }

        applyColor(current: number, target: number) {
            if(current <= 0) {
                return "red";
            }

            if (Math.abs(target - current) > 8) {
                return "yellow";
            }

            return "green";
        }

        closeModal() {
            this.isShipmentVisible = false;
            this.isNewProductVisible = false;
        }

        saveNewProduct(product: IProduct) {
            console.log('saveNewProduct', product)
        }

        async saveNewShipment(shipment: IShipment) {
            await inventoryService.updateInventoryQuantity(shipment);
            this.isShipmentVisible = false;
            await this.initialize();
        }

        showNewProductModal() {
            this.isNewProductVisible = true
        }

        showNewShipmentModal() {
            this.isShipmentVisible = true
        }

        async initialize() {
            this.inventory = await inventoryService.getInventory();
        }

        async created() {
            await this.initialize();
        }
    }
</script>

<style scoped lang="scss">
    @import "../scss/global";

    .green {
        font-weight: bold;
        color: $solar-green;
    }

    .yellow {
        font-weight: bold;
        color: $solar-yellow;
    }

    .red {
        font-weight: bold;
        color: $solar-red;
    }

    .inventory-actions {
        display: flex;
        margin-bottom: 0.8rem;
    }

    .product-archive {
        cursor: pointer;
        font-weight: bold;
        font-size: 1.2rem;
        color: $solar-red;
    }
</style>