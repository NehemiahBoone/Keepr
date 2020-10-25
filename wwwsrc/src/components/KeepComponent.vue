<template>
  <div class="col-6 col-md-3 card" data-toggle="modal" data-target="#keepModal">
    <img :src="keepProp.img" alt="" />
    <h2>{{ keepProp.name }}</h2>
    <img @click="viewProfile" :src="keepProp.creator.picture" alt="" />

    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">
              {{ keepProp.name }}
            </h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body text-center">
            {{ keepProp.description }}
            <br /><br />
            {{ keepProp.views }} | {{ keepProp.keeps }} <br /><br />
            <img :src="keepProp.img" alt="" />
          </div>
          <div class="modal-footer">
            <button class="btn btn-primary">Add to Vault</button>
            {{ keepProp.creator.name }}
            <img :src="keepProp.creator.picture" alt="" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "keep-component",
  props: ["keepProp"],
  data() {
    return {};
  },
  methods: {
    viewProfile() {
      this.$router.push({
        name: "Profile",
        params: { id: this.keepProp.creator.id },
      });
    },
  },
};
</script>

<style>
</style>