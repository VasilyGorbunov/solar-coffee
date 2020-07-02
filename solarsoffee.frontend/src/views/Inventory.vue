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
                <td>{{ item.quantityOnHand }}</td>
                <td>{{ item.product.price | price}}</td>
                <td>
                    <span v-if="item.product.isTaxable">Yes</span>
                    <span v-else>No</span>
                </td>
                <td>
                    <div>X</div>
                </td>
            </tr>
        </table>
        <new-product-modal
          v-if="isNewProductVisible"
          @close="closeModal"
        />
        <shipment-modal
          v-if="isShipmentVisible"
          :inventory="inventory"
          @close="closeModal"
        />
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator'
    import {IProductInventory} from "@/types/Product";
    import SolarButton from "@/components/SolarButton.vue";
    import ShipmentModal from "@/components/modals/ShipmentModal.vue";

    @Component({
        name: "Inventory",
        components: {ShipmentModal, SolarButton}
    })
    export default class Inventory extends Vue {
        isNewProductVisible: boolean = false;
        isShipmentVisible: boolean = false;

        inventory: IProductInventory[] = [
            {
                id: 1,
                product: {
                    id: 1,
                    name: 'Some Product',
                    description: 'Good stuff',
                    price: 100,
                    createdOn: new Date(),
                    updatedOn: new Date(),
                    isTaxable: true,
                    isArchived: false
                },
                quantityOnHand: 100,
                idealQuantity: 100
            },
            {
                id: 2,
                product: {
                    id: 2,
                    name: 'Another Product',
                    description: 'Good stuff',
                    price: 100,
                    createdOn: new Date(),
                    updatedOn: new Date(),
                    isTaxable: true,
                    isArchived: false
                },
                quantityOnHand: 40,
                idealQuantity: 20
            }
        ];

        closeModal() {
            this.isShipmentVisible = false;
            this.isNewProductVisible = false;
        }

        showNewProductModal() {

        }

        showShipmentModal() {

        }
    }
</script>

<style scoped lang="scss">

</style>