<template>
  <div class="home container-fluid">
    <div class="card-columns">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
  </div>
</template>

<script>
import keepComponent from "../components/KeepComponent";
export default {
  mounted() {
    this.$store.dispatch("getKeeps");
  },
  name: "home",
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
  },
  components: { keepComponent },
};
</script>

<style scoped>
@media (min-width: 576px) {
  .card-columns {
    column-count: 2;
  }
}

@media (min-width: 768px) {
  .card-columns {
    column-count: 3;
  }
}

@media (min-width: 992px) {
  .card-columns {
    column-count: 4;
  }
}
</style>