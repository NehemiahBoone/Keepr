<template>
  <div class="home container-fluid">

    <div class="row">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"/>
    </div>

  </div>
</template>

<script>
import keepComponent from "../components/KeepComponent"
export default {
  mounted(){
    this.$store.dispatch("getKeeps")
  },
  name: "home",
  computed: {
    keeps(){
      return this.$store.state.keeps
    }
  },
  components: {keepComponent}
};
</script>
