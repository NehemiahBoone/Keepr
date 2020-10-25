<template>

    <div  class="container-fluid">
        <div class="row">
            <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"/>
        </div>
    </div>
  
</template>

<script>
export default {
    mounted(){
        this.$store.dispatch("getSearchedProfile", this.$route.params.profileId)
        this.$store.dispatch("getProfileKeeps", this.$route.params.profileId);
    },
    name: "profile",
    computed: {
        keeps(){
            return this.$store.state.profileKeeps;
        },
        profile(){
            return this.$store.state.searchedProfile
        }
    }


}
</script>

<style>

</style>