<template>
  <div class="col-6 col-md-3 card" data-toggle="modal" data-target="#keepModal">
    <img :src="keepProp.img" alt="" />
    <h2>{{ keepProp.name }}</h2>
    <img @click="viewProfile" :src="keepProp.creator.picture" alt="" />
    <i
      class="fa fa-trash"
      v-if="keepProp.creatorId == this.$auth.userInfo.id"
      aria-hidden="true"
      @click="deleteKeep"
    ></i>

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
            {{ this.keepProp.views }} | {{ keepProp.keeps }} <br /><br />
            <img :src="keepProp.img" alt="" />
          </div>
          <div class="modal-footer">
            <button class="btn btn-primary" @click="viewKeep">View</button>
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
    viewKeep() {
      let viewedKeep = this.keepProp;
      viewedKeep.views++;
      this.$store.dispatch("viewKeep", viewedKeep);
    },
    deleteKeep() {
      let popup = confirm("Are you sure you want to delete this keep?");
      if (popup == true) {
        if (this.$route.name == "Profile") {
          this.$store.dispatch("deleteKeepOnProfile", {
            keep: this.keepProp,
            profileId: this.$route.params.id,
          });
        } else {
          this.$store.dispatch("deleteKeepOnHome", this.keepProp.id);
        }
      }
    },
  },
};
</script>

<style>
</style>